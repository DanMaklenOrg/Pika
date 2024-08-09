using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper.ShapezIo;

public class ShapezScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapperOld
{
    private readonly uint _steamAppId = 1318690;

    public ResourceId DomainId => "shapez";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain(DomainId, "Shapez")
        {
            Entities = await steamScrapperHelper.ScrapAchievements(_steamAppId, "achievement")
        };
    }
}
