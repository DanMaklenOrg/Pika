using HtmlAgilityPack;
using Pika.Model;

namespace Pika.DomainData.Scrapper.Palworld;

public class PalworldPalsAchievements(EntityNameContainer nameContainer, SteamClient steamClient) : IScrapper
{
    private readonly uint _steamAppId = 1623730;

    public DomainId DomainId => "palworld";
    public string OutputDirectory => "Palworld";
    public string FileName => "Achievements";

    public async Task<Domain> Scrape()
    {
        return new Domain
        {
            Id = DomainId,
            Name = "Palworld",
            Entities = await ScrapeAchievements(),
        };
    }

    private async Task<List<Entity>> ScrapeAchievements()
    {
        var steamAchievements = await ScrapeSteamAchievements();

        var achievements = steamAchievements.Select(rawName =>
        {
            var name = nameContainer.RegisterAndNormalize(rawName, "Achievement");
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, DomainId),
                Name = name,
                Classes = ["_/achievement"],
            };
        }).ToList();

        return achievements;
    }

    private async Task<HashSet<string>> ScrapeSteamAchievements()
    {
        var steamRawAchievements = await steamClient.GetAchievements(_steamAppId);
        var achievements = steamRawAchievements.Select(a => EntityNameContainer.Normalize(a.DisplayName)).ToHashSet();
        return achievements;
    }
}
