using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

namespace Pika.DomainData.DomainScrapper;

public class HadesScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1145360;

    public ResourceId DomainId => "hades";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
        domain.Entities.AddRange(await ScrapeKeepsakes());
        domain.Entities.AddRange(await ScrapeProphecies());
        domain.Entities.AddRange(await ScrapeMirrorAbilities());
        domain.Entities.AddRange(await ScrapeCodexEntries());
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
}
