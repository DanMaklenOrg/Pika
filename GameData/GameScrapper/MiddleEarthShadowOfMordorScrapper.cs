using HtmlAgilityPack;
using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class MiddleEarthShadowOfMordorScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 241930;

    public ResourceId GameId => "middle_earth_shadow_of_mordor";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId));
        game.Entities.AddRange(await ScrapeAbilities());
        game.Entities.AddRange(await ScrapeTutorialsAndHints());
    }

    private async Task<List<Entity>> ScrapeAbilities()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://shadowofwar.fandom.com/wiki/Abilities");
        var nodes = doc.DocumentNode.SelectNodes("//li/b/a");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "ability");
            return new Entity(id, name, "ability");
        }).ToList();
    }

    private async Task<List<Entity>> ScrapeTutorialsAndHints()
    {
        var doc = await new HtmlWeb().LoadFromWebAsync("https://shadowofwar.fandom.com/wiki/Tutorials_and_Hints");
        var nodes = doc.DocumentNode.SelectNodes("//span[@class='mw-headline']");
        return nodes.Select(n =>
        {
            var name = ScrapperHelper.CleanName(n.InnerText);
            var id = ScrapperHelper.InduceIdFromName(name, "hint");
            return new Entity(id, name, "hint");
        }).ToList();
    }
}
