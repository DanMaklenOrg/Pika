using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class HadesScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1145360;

    public ResourceId GameId => "hades";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
        game.Entities.AddRange(await ScrapeKeepsakes());
        game.Entities.AddRange(await ScrapeProphecies());
        game.Entities.AddRange(await ScrapeMirrorAbilities());
        game.Entities.AddRange(await ScrapeCodexEntries());
        game.Entities.AddRange(await ScrapeCompanions());
        game.Entities.AddRange(await ScrapeContractorUpgrades());

        await ScrapeResourceDirectorRanks(game);
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
            ParseAttribute(n, 0),
            ParseAttribute(n, 4),
        ]).ToList();

        Entity ParseAttribute(HtmlNode n, int columnOffset)
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode($"./td[{1 + columnOffset}]").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "mirror");
            var maxRank = ParseMaxRank(n.SelectSingleNode($"./td[{4 + columnOffset}]"));
            return new Entity(id, name, "mirror_ability") { Attributes = [new("max_rank", maxRank)] };
        }

        int ParseMaxRank(HtmlNode n) => n.InnerText.Count(c => c == '/') + 1;
    }

    private async Task<List<Entity>> ScrapeCodexEntries()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Codex");
        var nodes = doc.DocumentNode.SelectNodes("//td");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "codex");
            return new Entity(id, name, "codex_entry");
        }).ToList();
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

    private async Task<List<Entity>> ScrapeContractorUpgrades()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/House_Contractor");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[position() >= 2 and position()<=7]/tr/td[1]");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "contractor");
            return new Entity(id, name, "contractor_upgrade");
        }).ToList();
    }

    private async Task ScrapeResourceDirectorRanks(Game game)
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://hades.fandom.com/wiki/Resource_Director");
        var nodes = doc.DocumentNode.SelectNodes("(//tbody)[1]/tr/td[2]");
        var ranks = nodes.Select(n => ScrapperHelper.CleanName(n.InnerText)).ToList();
        game.Classes.Add(new("resource_director", "Resource Director")
        {
            Stats = [new("rank", "Rank", Stat.StatType.OrderedEnum) { EnumValues = ranks }]
        });
        game.Entities.Add(new("resource_director_entity", "Resource Director", "resource_director"));
    }
}
