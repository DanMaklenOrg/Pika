using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public class MiddleEarthShadowOfMordorScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 241930;

    public ResourceId DomainId => "middle_earth_shadow_of_mordor";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
