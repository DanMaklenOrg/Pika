using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public class Nodebuster(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 3107330;

    public ResourceId DomainId => "nodebuster";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
