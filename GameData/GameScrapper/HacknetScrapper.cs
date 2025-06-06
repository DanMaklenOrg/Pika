using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class HacknetScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 365450;

    public ResourceId GameId => "hacknet";

    public async Task ScrapeInto(Game game)
    {
        game.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
