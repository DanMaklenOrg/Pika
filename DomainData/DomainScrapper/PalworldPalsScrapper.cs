using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public class PalworldPalsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1623730;

    public ResourceId DomainId => "palworld";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
        domain.Entities.AddRange(await ScrapePals());
    }

    private async Task<List<Entity>> ScrapePals()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://palworld.wiki.gg/wiki/Pals");
        var nodes = doc.DocumentNode.SelectNodes("//td/span[@class='iconlink']/../..");
        HashSet<string> blacklist = ["013B: Gumoss (Special)", "092B: Warsect Terra"];
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode("td/span").InnerText);
            var palIndex = ScrapperHelper.CleanName(n.SelectSingleNode("td[2]").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "pal");
            return new Entity(id, $"{palIndex}: {name}", "pal");
        }).Where(e => !blacklist.Contains(e.Name)).ToList();
    }
}
