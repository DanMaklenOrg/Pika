using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class MiddleEarthShadowOfWarScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 356190;

    public ResourceId GameId => "middle_earth_shadow_of_war";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement_c"));
    }
}
