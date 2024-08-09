using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Palworld;

public class PalworldAchievements(SteamScrapper steamScrapper) : IScrapper
{
    private readonly uint _steamAppId = 1623730;

    public ResourceId DomainId => "palworld";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "Achievements";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Palworld",
            Entities = await steamScrapper.ScrapAchievements(DomainId, _steamAppId)
        };
    }
}
