using Pika.DomainData.ScrapperHelpers;
using Pika.Model;

namespace Pika.DomainData.DomainScrapper.DungeonSouls;

public class DungeonSoulsScrapper(SteamScrapperHelper steamScrapperHelper) : IScrapperOld
{
    private readonly uint _steamAppId = 383230;

    public ResourceId DomainId => "dungeon_souls";
    public string OutputDirectory => DomainId.ToString();
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain(DomainId, "Dungeon Souls")
        {
            Entities = await steamScrapperHelper.ScrapAchievements(_steamAppId, "achievement")
        };
    }
}
