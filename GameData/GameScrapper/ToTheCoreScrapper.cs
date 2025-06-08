using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class ToTheCoreScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 1988550;

    public ResourceId GameId => "to_the_core";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement_c"));
    }
}
