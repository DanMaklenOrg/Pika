using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class DwarfsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 35480;

    public ResourceId GameId => "dwarfs";

    public async Task ScrapeInto(Game game)
    {
        game.Achievements.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement_c"));
    }
}
