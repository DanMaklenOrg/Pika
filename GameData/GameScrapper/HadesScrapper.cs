using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class HadesScrapper : IScrapper
{
    public ResourceId GameId => "hades";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await ScrapeKeepsakes());
        game.Entities.AddRange(await ScrapeProphecies());
        game.Entities.AddRange(await ScrapeMirrorAbilities());
        game.Entities.AddRange(await ScrapeCodexEntries());
        game.Entities.AddRange(await ScrapeCompanions());
        game.Entities.AddRange(await ScrapeContractorUpgrades(game));
    }

    private async Task<List<Entity>> ScrapeKeepsakes()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Keepsakes");
        var nodes = doc.DocumentNode.SelectNodes("//table[@class='fandom-table']/tbody/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "keepsake");
            return new Entity(id, name, "keepsake");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeProphecies()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Fated_List_of_Minor_Prophecies");
        var nodes = doc.DocumentNode.SelectNodes("//table[contains(@class, 'wikitable')]/tbody/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "prophecy");
            return new Entity(id, name, "prophecy");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeMirrorAbilities()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Mirror_of_Night");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[2]/tr/td/..");
        return nodes.SelectMany<HtmlNode, Entity>(n =>
        [
            ParseAbility(n, 0, "mirror_red"),
            ParseAbility(n, 4, "mirror_green"),
        ]).ToList();

        Entity ParseAbility(HtmlNode n, int columnOffset, ResourceId tag)
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode($"./td[{1 + columnOffset}]").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "mirror");
            return new Entity(id, name, "mirror_ability") { Tags = [tag] };
        }
    }

    private async Task<List<Entity>> ScrapeCodexEntries()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Codex");
        var nodes = doc.DocumentNode.SelectNodes("//table");
        return nodes.SelectMany(groupNode =>
        {
            var tag = ParseTag(groupNode);
            return groupNode.SelectNodes(".//td").Select(n =>
            {
                var name = ScrapperHelper.CleanName(n.InnerText);
                if (name == "Lord Hades") name = "Hades";
                var id = ScrapperHelper.InduceIdFromName(name, "codex");
                return new Entity(id, name, "codex_entry") { Tags = [tag] };
            });
        }).ToList();

        ResourceId ParseTag(HtmlNode n)
        {
            var groupTitle = n.SelectSingleNode("./caption").InnerText.Trim();
            return groupTitle switch
            {
                "Chthonic Gods" => "codex_chthonic",
                "Olympian Gods" => "codex_olympian",
                "Others of Note" => "codex_others",
                "The Underworld" => "codex_underworld",
                "Infernal Arms" => "codex_infernal_arms",
                "Perilous Foes" => "codex_foes",
                "Artifacts" => "codex_artifacts",
                "River Denizens" => "codex_river",
                "Fables" => "codex_fables",
                _ => throw new Exception($"Unknown tag value {groupTitle}"),
            };
        }
    }

    private async Task<List<Entity>> ScrapeCompanions()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Companions");
        var nodes = doc.DocumentNode.SelectNodes("(//table)[1]/tbody/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "companion");
            return new Entity(id, name, "companion");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeContractorUpgrades(Game game)
    {
        List<ResourceId> tagMap =
        [
            "contractor_orders",
            "contractor_great_hall",
            "contractor_west_hall",
            "contractor_lounge",
            "contractor_bedchambers",
            "contractor_music",
        ];
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/House_Contractor");
        var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'wds-tab__content')]/table");
        return nodes.SelectMany((nodeGroup, i) => nodeGroup.SelectNodes("./tbody/tr/td[1]").Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            name = name switch
            {
                "Pitch-Black Darkness" => "Darkness, Pitch-Black",
                "Fated Keys" => "Chthonic Keys, Fated",
                "Vintage Nectar" => "Nectar, Vintage",
                "Brilliant Gemstones" => "Gemstones, Brilliant",
                "Final Expense (Payback)" => "Final Expense (Payback Mix)",
                _ => name,
            };
            if (name.StartsWith("Rug,")) name = $"{name} ({game.Tags.Single(t => t.Id == tagMap[i]).Name})";
            var id = ScrapperHelper.InduceIdFromName(name, "contractor");
            return new Entity(id, name, "contractor_upgrade") { Tags = [tagMap[i]]};
        })).ToList();
    }
}
