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
        game.Entities.AddRange(await ScrapeAccessories());
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var items = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/map_data_en.js", PalDbMapDataVariable);
        var worldTreeItems = await jsScraper.ScrapeJsVariable("https://paldb.cc/js/treemap_data_en.js", PalDbMapDataVariable);
        HashSet<string> fieldBosses = items.Union(worldTreeItems)
            .Where(x => x["type"]!.GetValue<string>() == "Alpha Pal")
            .Select(x => ScrapperHelper.CleanName(x["item"]!.GetValue<string>()))
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
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Mission");
        var nodes = doc.DocumentNode.SelectNodes("//div[@class='col']/div/div[2]/div");
        int palCriticRequestCount = 1;
        return nodes
            .Select(n =>
            {
                var name = ScrapperHelper.CleanName(n.SelectSingleNode("./div[1]").InnerText);
                var missionType = ScrapperHelper.CleanName(n.SelectSingleNode("./div[2]").InnerText);
                if (missionType == "Sub Mission" && name == string.Empty)
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
            })
            .Where(e => e.Name is not "" and not "Fill Your Belly" and not "Put Pals to Work")
            .DistinctBy(e => e.Id)
            .ToList();
    }

    private async Task<List<Entity>> ScrapeImplants()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://paldb.cc/en/Pal_Surgery_Table#Surgery");
        var nodes = doc.DocumentNode.SelectNodes("//table/tbody/tr/td[2 and contains(., 'Implant') and not(.//i)]//a");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText).Replace("Implant: ", string.Empty);
            var id = ScrapperHelper.InduceIdFromName(name, "implant");
            return new Entity(id, name, "implant");
        }).Where(i => !i.Name.Contains("Disposable")).ToList();
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
                && !n.InnerText.StartsWith("'s")
                && !n.InnerText.EndsWith("Effigy")
                && !n.InnerText.StartsWith("Key Sphere of")
                && !n.InnerText.EndsWith("Harness")
                && !n.InnerText.EndsWith("Necklace")
                && !n.InnerText.EndsWith("Gloves")
                && !n.InnerText.EndsWith("Launcher")
                && !n.InnerText.EndsWith("Launcher")
                && n.InnerText is not
                    "Tanzee's Assault Rifle" and not
                    "Tanzee Ignis's Assault Rifle" and not
                    "Modified Pal's Contaminated Core" and not
                    "Bastigor's Hammer" and not
                    "Lifmunk's Submachine Gun" and not
                    "Hangyu Cryst's Glove" and not
                    "Grizzbolt's Minigun" and not
                    "Nyafia's Shotgun" and not
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
}
