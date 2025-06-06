using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class CrossCellsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 632000;

    public ResourceId GameId => "crosscells";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
