using Pika.Model;

namespace Pika.DomainData.Scrapper.DungeonSouls;

public class DungeonSoulsScrapper(SteamScrapper steamScrapper) : IScrapper
{
    private readonly uint _steamAppId = 1623730;

    public DomainId DomainId => "dungeon_souls";
    public string OutputDirectory => "DungeonSouls";
    public string FileName => DomainId.ToString();

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Dungeon Souls",
            Entities = await steamScrapper.ScrapAchievements(DomainId, _steamAppId)
        };
    }
}
