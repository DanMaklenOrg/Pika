using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

namespace Pika.DomainData.DomainScrapper;

public class CrossCellsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 632000;

    public ResourceId DomainId => "crosscells";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
