using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class HexcellsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 265890;

    public ResourceId GameId => "hexcells";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
