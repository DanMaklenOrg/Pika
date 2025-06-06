using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public class MiddleEarthShadowOfWarScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 356190;

    public ResourceId DomainId => "middle_earth_shadow_of_war";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
