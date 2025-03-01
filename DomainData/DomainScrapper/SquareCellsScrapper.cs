using HtmlAgilityPack;
using Pika.DomainData.ScrapperHelpers;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

namespace Pika.DomainData.DomainScrapper;

public class SquareCellsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapper
{
    private const uint SteamAppId = 416770;

    public ResourceId DomainId => "squarecells";

    public async Task ScrapeInto(Domain domain)
    {
        domain.Entities.AddRange(await steamScrapperHelper.ScrapAchievements(SteamAppId, "achievement"));
    }
}
