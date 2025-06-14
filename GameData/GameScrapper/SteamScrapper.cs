using Pika.GameData.ScrapperHelpers;
using Pika.Model;

namespace Pika.GameData.GameScrapper;

public class SteamScrapper(SteamClient client)
{
    public async Task ScrapeInto(Game game, uint steamAppId)
    {
        var achievements = await client.GetAchievements(steamAppId);
        game.Achievements.AddRange(achievements.Select(a =>
        {
            var name = ScrapperHelper.CleanName(a.DisplayName);
            var id = ScrapperHelper.InduceIdFromName(name, "achievement");
            return new Achievement(id, name) { Description = a.Description };
        }));
    }
}
