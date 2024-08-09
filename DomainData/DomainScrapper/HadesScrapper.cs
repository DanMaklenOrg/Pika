using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper.Hades;

public class HadesScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private readonly uint _steamAppId = 1145360;

    public ResourceId DomainId => "hades";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(_steamAppId, "achievement"));
        domain.Entities.AddRange(await ScrapeKeepsakes());
        domain.Entities.AddRange(await ScrapeProphecies());
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
}
