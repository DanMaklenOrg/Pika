using Pika.Model;

namespace Pika.GameData.ScrapperHelpers;

public class SteamScrapperHelper(SteamClient client)
{
    public async Task<List<Entity>> ScrapAchievements(uint steamAppId, ResourceId classId, string? idPrefix = "achievement", Dictionary<string, string>? rename = null)
    {
        var achievements = await client.GetAchievements(steamAppId);
        return achievements.Select(a =>
        {
            var name = ScrapperHelper.CleanName(a.DisplayName);
            if (rename is not null && rename.TryGetValue(name, out var value)) name = value;
            var id = ScrapperHelper.InduceIdFromName(name, idPrefix);
            return new Entity(id, name, classId);
        }).ToList();
    }
}
