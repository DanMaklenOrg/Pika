using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper.Palworld;

public class PalworldAchievements(SteamScrapperHelper steamScrapperHelper) : IScrapperOld
{
    private readonly uint _steamAppId = 1623730;

    public ResourceId DomainId => "palworld";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => "Achievements";

    public async Task<Domain> Scrape()
    {
        return new Domain(DomainId, "Palworld")
        {
            Entities = await steamScrapperHelper.ScrapAchievements(_steamAppId, "achievement"),
        };
    }
}
