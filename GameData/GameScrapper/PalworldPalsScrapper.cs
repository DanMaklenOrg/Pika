using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class PalworldPalsScrapper : IScrapper
{
    private static readonly Regex PalIndexIdRegex = new Regex(@"^#(\d{1,3})(\D)?$");

    public ResourceId GameId => "palworld";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await ScrapePals());
        game.Entities.AddRange(await ScrapeTerrariaCreatures());
        game.Entities.AddRange(await ScrapeMissions());
        game.Entities.AddRange(await ScrapeFieldBosses());
        game.Entities.AddRange(await ScrapeWantedFugitives());
        game.Entities.AddRange(await ScrapeTowerBosses());
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='break border']/preceding-sibling::div");
        return nodes.Select(n =>
        {
            var palIndexRaw = ScrapperHelper.CleanName(n.SelectSingleNode(".//span").InnerText);
            var match = PalIndexIdRegex.Match(palIndexRaw);
            var palId = int.Parse(match.Groups[1].Value);
            var palVariant = match.Groups[2].Success ? match.Groups[2].Value : string.Empty;
            var palIndex = $"#{palId:D3}{palVariant}";
            var palName = ScrapperHelper.CleanName(n.SelectSingleNode(".//a[@class='itemname']").InnerText);
            var name = $"{palIndex}: {palName}";
            var id = ScrapperHelper.InduceIdFromName(name, "pal");
            return new Entity(id, name, "pal");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeTerrariaCreatures()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='break border']/following-sibling::div");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode(".//a[@class='itemname']").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "terraria_creature");
            return new Entity(id, name, "terraria_creature");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeMissions()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Mission");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col']/div/div[2]/div");
        int palCriticRequestCount = 1;
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode("./div[1]").InnerText);
            var missionType = ScrapperHelper.CleanName(n.SelectSingleNode("./div[2]").InnerText);
            if (name == string.Empty)
            {
                name = $"Request from Pal Critic ({palCriticRequestCount++})";
            }

            var tag = missionType switch
            {
                "Main Mission" => "mission_main",
                "Sub Mission" => "mission_sub",
                _ => throw new NotSupportedException(),
            };
            var id = ScrapperHelper.InduceIdFromName(name, "mission");
            return new Entity(id, name, "mission") { Tags = [tag] };
        }).Where(e => e.Name != "How to Stay Alive").ToList();
    }

    private async Task<List<Entity>> ScrapeFieldBosses()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Alpha_Pals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col' and contains(., 'Bounty Token')]/div/div/div/a[@class='itemname']");
        return nodes.Select(n =>
        {
            var name = RenameFieldBoss(ScrapperHelper.CleanName(n.InnerText));
            var id = ScrapperHelper.InduceIdFromName(name, "field_boss");
            return new Entity(id, name, "field_boss");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeWantedFugitives()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Humans");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col' and not(.//i) and not(contains(., 'Chopper'))]//a[@class = 'itemname']");
        return nodes.Select(n =>
        {
            var name = RenameWantedFugitive(ScrapperHelper.CleanName(n.InnerText));
            var id = ScrapperHelper.InduceIdFromName(name, "wanted_fugitive");
            return new Entity(id, name, "wanted_fugitive");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeTowerBosses()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Tower");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col' and contains(., 'Normal')]//a[@class = 'itemname']");
        return nodes.Select(n =>
        {
            var name = RenameTowerBoss(ScrapperHelper.CleanName(n.InnerText));
            var id = ScrapperHelper.InduceIdFromName(name, "tower_boss");
            return new Entity(id, name, "tower_boss");
        }).ToList();
    }

    #region RenameFunctions
    private static string RenameFieldBoss(string oldName)
    {
        return oldName switch
        {
            "Icy Blossom Voyager Foxparks Cryst" => "Foxparks Cryst",
            "Pioneer of the Frozen Sea Penking" => "Penking",
            "Suddenly Transformed Gumoss" => "Gumoss",
            "Inheritor of the Storm Mossanda Lux" => "Mossanda Lux",
            "Tainted Farm Caprity Noct" => "Caprity Noct",
            "Wings of the Firmament Nitewing" => "Nitewing",
            "Sparkling Smile Ribbuny Botan" => "Ribbuny Botan",
            "Perpetual Procrastinator Dumud" => "Dumud",
            "Enraptured by the Frozen Moonlight Loupmoon Cryst" => "Loupmoon Cryst",
            "Empress of the Hive Elizabee" => "Elizabee",
            "Marshmallow Body Grintale" => "Grintale",
            "King of the Floof Sweepa" => "Sweepa",
            "Dancer on the Steppe Chillet" => "Chillet",
            "Swift Deity Univolt" => "Univolt",
            "Guardian of the Dark Flame Kitsun Noct" => "Kitsun Noct",
            "Born of the Thunderclouds Dazzi Noct" => "Dazzi Noct",
            "Extraterrestrial Lunaris" => "Lunaris",
            "Guardian of Lightning Dinossom Lux" => "Dinossom Lux",
            "Vagrant Warrior Bushi" => "Bushi",
            "Wings of Thunder Beakon" => "Beakon",
            "Phantasmal Feline Katress" => "Katress",
            "Gale of the Forest Verdash" => "Verdash",
            "Voice of the Violets Vaelet" => "Vaelet",
            "Pallid Lady Sibelyx" => "Sibelyx",
            "Gentle Sky Dragon Elphidran" => "Elphidran",
            "Lady of the Lake Azurobe" => "Azurobe",
            "Predator of the Earth Cryolinx Terra" => "Cryolinx Terra",
            "Gluttonous Thunder Dragon Relaxaurus Lux" => "Relaxaurus Lux",
            "Winds of Spring Broncherry" => "Broncherry",
            "Waves of Summer Broncherry Aqua" => "Broncherry Aqua",
            "Lady of the Flower Garden Petallia" => "Petallia",
            "Supreme Fluff Commander Kingpaca" => "Kingpaca",
            "Azure Fluff Commander Kingpaca Cryst" => "Kinpaca Cryst",
            "King of the Forest Mammorest" => "Mammorest",
            "Guardian of the Grassy Fields Wumpo Botan" => "Wumpo Botan",
            "Unyielding Colossus Warsect" => "Warsect",
            "Golden Armor Warrior Warsect Terra" => "Warsect Terra",
            "Drifting Cloud Fenglope" => "Fenglope",
            "Drifting Thundercloud Fenglope Lux" => "Fenglope Lux",
            "Gloom-Shrouded Bloodsucker Felbat" => "Felbat",
            "Snow-White Soarer Quivern" => "Quivern",
            "Cursed Tyrant Blazamut" => "Blazamut",
            "Monarch of the Steel Dragon Astegon" => "Astegon",
            "Unstoppable Stinger Menasting" => "Menasting",
            "Gold-Piercing Spear Menasting Terra" => "Menasting Terra",
            "Guardian of the Dark Sun Anubis" => "Anubis",
            "Emperor of the Sea Jormuntide" => "Jormuntide",
            "Ruler of the Crimson Dawn Suzaku" => "Suzaku",
            "Empress of the Abyss Lyleen Noct" => "Lyleen Noct",
            "Sentinel of the Tides Faleris Aqua" => "Faleris Aqua",
            "Holy Knight of Legend Paladius" => "Paladius",
            "Legendary Steed of Ice Frostallion" => "Frostallion",
            "Legendary Steed of Darkness Frostallion Noct" => "Frostallion Noct",
            "Legendary Celestial Dragon Jetragon" => "Jetragon",
            "Suit of Armor Knocklem" => "Knocklem",
            "Soul of the Dark Rabbit Nitemary" => "Nitemary",
            "Midnight Blue Mane Starryon" => "Starryon",
            "Guardian Dragon of Ironclad Silvegis" => "Silvegis",
            "Jet-Black Little Hero Smokie" => "Smokie",
            "Beast of Salvation Celesdir" => "Celesdir",
            "Hundred-Faced Apostle Omascul" => "Omascul",
            "Crimson Butcher Splatterina" => "Splatterina",
            "Tactician of the Thread Realm Tarantriss" => "Tarantriss",
            "Blue Lightning Steed Azurmane" => "Azurmane",
            "Indigo Handmaiden Prunelia" => "Prunelia",
            "Night Shade Hunter Nyafia" => "Nyafia",
            "Guardian Beast of Twilight Gildane" => "Gildane",
            "Nice Ice Lance Whalaska" => "Whalaska",
            "Nice Crimson Lance Whalaska Ignis" => "Whalaska Ignis",
            "Legend of the Seas Neptilius" => "Neptilius",
            _ => oldName,
        };
    }

    private static string RenameWantedFugitive(string oldName)
    {
        return oldName switch
        {
            "Amateur Hunter Hawk" => "Hawk",
            "Intimidating Interviewer Grill" => "Grill",
            "Gloating Narcissist Ego" => "Ego",
            "Hostile Defender Brick" => "Brick",
            "Workplace Tyrant Whip" => "Whip",
            "Black Magic Fanatic Shadow" => "Shadow",
            "Kidnapper Whisk" => "Whisk",
            "Blundering Klutz Fumble" => "Fumble",
            "Street Assassin Urchin" => "Urchin",
            "Cannibal Gnaw" => "Gnaw",
            "Human Collector Cache" => "Cache",
            "Scam Artist Jade" => "Jade",
            "Attention-Seeking Monster Dazzle" => "Dazzle",
            "Pineapple Pizza Enthusiast Aloha" => "Aloha",
            "Dango Thief Nimble" => "Nimble",
            "Insurance Fraud Crash" => "Crash",
            "Walking Smoker Dart" => "Dart",
            "Trigger-Happy Gunslinger Clint" => "Clint",
            "Cattle Rustler Lasso" => "Lasso",
            "Pyromaniac Flare" => "Flare",
            "Serial Cheater Siren" => "Siren",
            "Bounty Hunter Hunter Turncoat" => "Turncoat",
            "Twin Bombers Dyna" => "Dyna",
            "Twin Bombers Mite" => "Mite",
            "Well-Meaning Quack Quill" => "Quill",
            "Dine-and-Dasher Scoot" => "Scoot",
            "Escape Artist Phantom" => "Phantom",
            "Chronic Spoiler Whisper" => "Whisper",
            "Serial Borrower Pinch" => "Pinch",
            "Breaking and Entering Ram" => "Ram",
            "Serial Embezzler Skim" => "Skim",
            "Counterfeiter Mimic" => "Mimic",
            "Horse Rustler Billy" => "Billy",
            _ => oldName,
        };
    }

    private static string RenameTowerBoss(string oldName)
    {
        return oldName switch
        {
            "Rayne Syndicate Officer Zoe & Grizzbolt" => "Zoe & Grizzbolt",
            "Free Pal Alliance Founder Lily & Lyleen" => "Lily & Lyleen",
            "PIDF Officer Marcus & Faleris" => "Marcus & Faleris",
            "Brothers of the Eternal Pyre Soul Leader Axel & Orserk" => "Axel & Orserk",
            "PAL Genetic Research Unit Commander Victor & Shadowbeak" => "Victor & Shadowbeak",
            "Leader of the Moonflowers Saya & Selyne" => "Saya & Selyne",
            "Jarl of Feybreak  Bjorn & Bastigor" => "Bjorn & Bastigor",
            _ => oldName,
        };
    }
    #endregion
}
