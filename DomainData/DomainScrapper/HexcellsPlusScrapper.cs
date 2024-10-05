using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

namespace Pika.DomainData.DomainScrapper;

public class HexcellsPlusScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 271900;

    public ResourceId DomainId => "hexcells_plus";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
