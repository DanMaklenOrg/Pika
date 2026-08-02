using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class PalworldScrapper(JsScrapperHelper jsScraper) : IScrapper
{
    private static readonly Regex PalIndexIdRegex = new Regex(@"^#(\d{1,3})(\D)?$");
    private static readonly string PalDbMapDataVariable = "fixedDungeon";

    public ResourceId GameId => "palworld";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await ScrapePals());
        game.Entities.AddRange(await ScrapeWantedFugitive());
        game.Entities.AddRange(await ScrapeTowerBosses());
        game.Entities.AddRange(await ScrapeGreatEagleStatues());
        game.Entities.AddRange(await ScrapeWatchTowers());
        game.Entities.AddRange(await ScrapeMissions());
        game.Entities.AddRange(await ScrapeImplants());
        game.Entities.AddRange(await ScrapePalGear());
        game.Entities.AddRange(await ScrapeKeyItems());
        game.Entities.AddRange(await ScrapTechnologies());
        game.Entities.AddRange(await ScrapeExpeditions());
        game.Entities.AddRange(await ScrapeAccessories());
        game.Entities.AddRange(await ScrapeResearch());
        game.Entities.AddRange(await ScrapeJournals());
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var items = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/map_data_en.js", PalDbMapDataVariable);
        var worldTreeItems = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/treemap_data_en.js", PalDbMapDataVariable);
        HashSet<string> fieldBosses = items.Union(worldTreeItems)
            .Where(x => x["type"]!.GetValue<string>() == "Alpha Pal")
            .Select(x => ScrapperHelper.CleanName(x["item"]!.GetValue<string>().Split('<').First()))
            .ToHashSet();

        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//a[@class='itemname']/..");
        return nodes.Select(n =>
        {
            var palNameRaw = ScrapperHelper.CleanName(n.SelectSingleNode(".//a[@class='itemname']").InnerText);
            var palIndexRaw = ScrapperHelper.CleanName(n.SelectSingleNode(".//span").InnerText);
            var match = PalIndexIdRegex.Match(palIndexRaw);
            var palIndex = "#ToT";

            if (match.Success)
            {
                var palId = match.Success ? int.Parse(match.Groups[1].Value) : -1;
                var palVariant = match.Groups[2].Success ? match.Groups[2].Value : string.Empty;
                palIndex = $"#{palId:D3}{palVariant}";
            }

            List<ResourceId> tags = [];
            if (fieldBosses.Contains(palNameRaw))
            {
                tags.Add("field_boss");
            }

            var name = $"{palIndex}: {palNameRaw}";
            var id = ScrapperHelper.InduceIdFromName(name, "pal");
            return new Entity(id, name, "pal") { Tags = tags };
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeWantedFugitive()
    {
        var items = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/map_data_en.js", PalDbMapDataVariable);
        return items
            .Where(x => x["type"]!.GetValue<string>() == "NPC" && x["id"]!.GetValue<string>().StartsWith("BOSS_"))
            .Select(x =>
            {
                var name = ScrapperHelper.CleanName(x["item"]!.GetValue<string>().Split(' ').Last());
                var id = ScrapperHelper.InduceIdFromName(name, "wanted_fugitive");
                return new Entity(id, name, "wanted_fugitive");
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeTowerBosses()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Tower");
        var nodes = doc.DocumentNode.SelectNodes("//span[@class='badge bg-danger']/../../a");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(string.Join(' ', n.InnerText.Split(' ')[^3..]));
            var id = ScrapperHelper.InduceIdFromName(name, "tower_boss");
            return new Entity(id, name, "tower_boss");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeGreatEagleStatues()
    {
        var items = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/map_data_en.js", PalDbMapDataVariable);
        var worldTreeItems = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/treemap_data_en.js", PalDbMapDataVariable);
        return items.Union(worldTreeItems)
            .Where(x => x["type"]!.GetValue<string>() == "Fast Travel")
            .Select(x =>
            {
                var name = ScrapperHelper.CleanName(x["item"]!.GetValue<string>());
                var id = ScrapperHelper.InduceIdFromName(name, "great_eagle_statue");
                return new Entity(id, name, "great_eagle_statue");
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeWatchTowers()
    {
        var items = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/map_data_en.js", PalDbMapDataVariable);
        var worldTreeItems = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/treemap_data_en.js", PalDbMapDataVariable);
        return items.Union(worldTreeItems)
            .Where(x => x["type"]!.GetValue<string>() == "Watchtower")
            .Select(x =>
            {
                var name = ScrapperHelper.CleanName(x["item"]!.GetValue<string>());
                var id = ScrapperHelper.InduceIdFromName(name, "watchtower");
                return new Entity(id, name, "watchtower");
            }).ToList();
    }

    private async Task<List<Entity>> ScrapeMissions()
    {
        HashSet<string> blacklist =
        [
            "A Cozy Base", "Base of Operations", "Capture any 30 Pals.", "Capturing Device", "First Boss Battle",
            "First Gathering Run", "Hunger is the Greatest Foe", "Mining Paldium", "Pal Eggs", "Palbox", "Paldium",
            "Partner Skills", "Path to the Abyss", "Safety Measures", "Sealed Pals", "Starting Base", "The Adventure Begins",
            "The Girl and the Tower", "Unlock Technology", "Wildlife Sanctuaries", "Your First Pal", "Protect Yourself",
            "Fill Your Belly", "Put Pals to Work",
        ];
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Mission");
        var nodes = doc.DocumentNode.SelectNodes("//div[@data-id]");
        int palCriticRequestCount = 1;
        return nodes
            .Where(n =>
            {
                var dataId = n.GetAttributeValue("data-id", string.Empty);
                var isMainMission =  dataId.StartsWith("Main_");
                var isOld = dataId.EndsWith("_Old");
                var isOldEnhanceStatsMission = dataId == "Main_GainStatus";
                var name = ScrapperHelper.CleanName(n.InnerText);
                return !isMainMission || !isOld && ! isOldEnhanceStatsMission && !blacklist.Contains(name);
            })
            .Select(n =>
            {
                var name = ScrapperHelper.CleanName(n.InnerText);
                var isMainMission =  n.GetAttributeValue("data-id", string.Empty).StartsWith("Main_");
                if (!isMainMission && name == string.Empty)
                {
                    name = $"Request from Pal Critic ({palCriticRequestCount++})";
                }

                var id = ScrapperHelper.InduceIdFromName(name, "mission");
                var tag = isMainMission ? "mission_main" : "mission_sub";
                return new Entity(id, name, "mission") { Tags = [tag] };
            })
            .ToList();
    }

    private async Task<List<Entity>> ScrapeImplants()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pal_Surgery_Table#Surgery");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col']//div[3]/a[@class='itemname']");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText).Replace("Implant: ", string.Empty);
            var id = ScrapperHelper.InduceIdFromName(name, "implant");
            return new Entity(id, name, "implant");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapePalGear()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pal_Gear_Workbench");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[2]//td/a/.");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "pal_gear");
            return new Entity(id, name, "pal_gear");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeKeyItems()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Key_Items");
        var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'hover_banner') and not(.//i)]//a");
        return nodes
            .Where(n =>
                !n.InnerText.StartsWith("Implant: ")
                && !n.InnerText.EndsWith("Bounty Token")
                && !n.InnerText.EndsWith("Saddle")
                && !n.InnerText.EndsWith("Effigy")
                && !n.InnerText.StartsWith("Key Sphere of")
                && !n.InnerText.EndsWith("Harness")
                && !n.InnerText.EndsWith("Necklace")
                && !n.InnerText.EndsWith("Gloves")
                && !n.InnerText.EndsWith("Launcher")
                && !n.InnerText.EndsWith("Echobone")
                && n.InnerText is not
                    "Tanzee's Assault Rifle" and not
                    "Tanzee Ignis's Assault Rifle" and not
                    "Modified Pal's Contaminated Core" and not
                    "Dandilord's Petal" and not
                    "Silvance's Plume" and not
                    "Bastigor's Hammer" and not
                    "Lifmunk's Submachine Gun" and not
                    "Hangyu Cryst's Glove" and not
                    "Grizzbolt's Minigun" and not
                    "Nyafia's Shotgun" and not
                    "Echoing Flute" and not
                    "Digtoise's Headband"
            ).Select(n =>
            {
                var name = ScrapperHelper.CleanName(n.InnerText);

                var (c, t) = name switch
                {
                    _ when name.EndsWith("Pouch") => ("inventory_upgrade", "inventory_upgrade_pouch"),
                    _ when name.Contains("Weapon Holster") => ("inventory_upgrade", "inventory_upgrade_holster"),
                    _ when name.EndsWith("Accessory Box") => ("inventory_upgrade", "inventory_upgrade_accessory"),
                    _ when name.EndsWith("Feed Bag") => ("inventory_upgrade", "inventory_upgrade_feed"),
                    _ when name.EndsWith("Feed Bag") => ("inventory_upgrade", "inventory_upgrade_feed"),
                    _ when name.EndsWith("Hip Lantern") => ("misc_tool", "misc_tool_lantern"),
                    _ when name.StartsWith("Lockpicking Tool") => ("misc_tool", "misc_tool_lockpick"),
                    _ when name.EndsWith("Construction Kit") => ("misc_tool", "misc_tool_misc"),
                    _ => throw new ArgumentOutOfRangeException(),
                };

                var id = ScrapperHelper.InduceIdFromName(name, c);
                return new Entity(id, name, c) { Tags = [t] };
            }).ToList();
    }

    private async Task<List<Entity>> ScrapTechnologies()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Technologies");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='hoverTechFooter']");
        return nodes.Select(n =>
        {
            var nameRaw = ScrapperHelper.CleanName(n.InnerText);
            var techLevel = int.Parse(ScrapperHelper.CleanName(n.SelectSingleNode("./../../div[1]").InnerText));
            var tag = n.ParentNode.HasClass("BossTechnology") ? "tech_ancient" : "tech_tech";

            var name = $"Lv. {techLevel:D2}: {nameRaw}";
            var id = ScrapperHelper.InduceIdFromName(name, "tech");
            return new Entity(id, name, "tech") { Tags = [tag] };
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeExpeditions()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pal_Expedition_Station#PalExpeditions");
        var nodes = doc.DocumentNode.SelectNodes("//h4");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "expedition");
            return new Entity(id, name, "expedition");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeAccessories()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Accessory");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col' and not(.//i)]//a[@class='itemname' and not(./img)]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "accessory");
            return new Entity(id, name, "accessory");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeResearch()
    {
        List<string> workSuitabilities = [ "Handiwork", "Kindling", "Watering", "Planting", "Generating_Electricity", "Lumbering", "Mining", "Cooling", "Medicine_Production" ];

        List<Entity> entities = [];

        foreach (var suitability in workSuitabilities)
        {
            var doc = await new HtmlWeb().LoadFromWebAsync($"https://paldb.cc/en/{suitability}#Research");
            var nodes = doc.DocumentNode.SelectNodes("//div[@class = 'col']/div/div[1]");
            entities.AddRange(nodes.Select(n =>
            {
                var nameRaw = ScrapperHelper.CleanName(n.SelectSingleNode("./div[1]").InnerText);
                var lvl = int.Parse(n.SelectSingleNode("./div[2]").InnerText.Split('.').Last());
                var tag = ScrapperHelper.InduceIdFromName(suitability, "work_suitability");
                var name = $"Lv. {lvl}: {nameRaw}";
                var id = ScrapperHelper.InduceIdFromName(name, "research");
                return new Entity(id, name, "research") { Tags = [tag] };
            }));
        }

        return entities;
    }

    private async Task<List<Entity>> ScrapeJournals()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Journals");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='card-body']//a");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "journal");
            return new Entity(id, name, "journal");
        }).ToList();
    }
}
