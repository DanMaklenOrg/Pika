using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public class DungeonSoulsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 383230;

    public ResourceId DomainId => "dungeon_souls";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
