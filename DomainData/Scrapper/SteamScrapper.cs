using Pika.Model;

namespace Pika.DomainData.Scrapper;

public class SteamScrapper(SteamClient client, EntityNameContainer nameContainer)
{
    public async Task<List<Entity>> ScrapAchievements(DomainId domainId, uint steamAppId)
    {
        var achievements = await client.GetAchievements(steamAppId);
        return achievements.Select(a =>
        {
            var name = nameContainer.RegisterAndNormalize(a.DisplayName, "Achievement");
            return new Entity
            {
                Id = ResourceId.InduceFromName(name, domainId),
                Name = name,
                Class = "_/achievement",
            };
        }).ToList();
    }
}
