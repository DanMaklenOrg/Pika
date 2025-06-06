using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class PalworldPalsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1623730;

    public ResourceId GameId => "palworld";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
        game.Entities.AddRange(await ScrapePals());
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
