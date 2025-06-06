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
        var doc = await new HtmlWeb().LoadFromWebAsync("https://palworld.fandom.com/wiki/Palpedia#Visible");
        var nodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'wds-tab__content')]/table/tbody/tr");
        HashSet<string> blacklist = ["013B: Gumoss (Special)", "092B: Warsect Terra"];
        return nodes.Where(n => !n.InnerHtml.Contains("<th>")).Where(n => n.SelectSingleNode("td[1]").InnerText != "???").Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.SelectSingleNode("td[3]").InnerText);
            var palIndex = ScrapperHelper.CleanName(n.SelectSingleNode("td[1]").InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "pal");
            return new Entity(id, $"{palIndex}: {name}", "pal");
        }).Where(e => !blacklist.Contains(e.Name)).ToList();
    }
}
