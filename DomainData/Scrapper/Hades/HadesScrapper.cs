using Pika.Model;

namespace Pika.DomainData.Scrapper.Hades;

public class HadesScrapper(SteamScrapper steamScrapper) : IScrapper
{
    private readonly uint _steamAppId = 1145360;

    public ResourceId DomainId => "hades";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Hades",
            Entities = await steamScrapper.ScrapAchievements(DomainId, _steamAppId)
        };
    }
}
