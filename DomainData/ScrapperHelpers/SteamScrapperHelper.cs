using Pika.Model;

namespace Pika.DomainData.ScrapperHelpers;

public class SteamScrapperHelper(SteamClient client)
{
    public async Task<List<Entity>> ScrapAchievements(uint steamAppId, ResourceId classId, string? idPrefix = "achievement")
    {
        var achievements = await client.GetAchievements(steamAppId);
        return achievements.Select(a =>
        {
            var name = ScrapperHelper.CleanName(a.DisplayName);
            var id = ScrapperHelper.InduceIdFromName(name, idPrefix);
            return new Entity(id, name, classId);
        }).ToList();
    }
}
