using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class SquareCellsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 416770;

    public ResourceId GameId => "squarecells";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement_c"));
    }
}
