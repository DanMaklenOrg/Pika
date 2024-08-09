using Pika.Model;

namespace Pika.DomainData.Scrapper.ShapezIo;

public class ShapezScrapper(SteamScrapper steamScrapper) : IScrapper
{
    private readonly uint _steamAppId = 1318690;

    public ResourceId DomainId => "shapez";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Shapez",
            Entities = await steamScrapper.ScrapAchievements(DomainId, _steamAppId)
        };
    }
}
