using Pika.DataLayer.Model;

namespace Pika.GameDataScrapper.SchemaFetchers;

public class DaveTheDiverSchemaFetcher : ISchemaFetcher
{
    private const uint AppId = 1868140;

    private readonly SteamClient _steamClient;

    public DaveTheDiverSchemaFetcher(SteamClient steamClient)
    {
        _steamClient = steamClient;
    }

    public async Task<GameSchema> FetchSchema(Guid pikaGameId)
    {
        return new GameSchema
        {
            Version = "v1.0.1.1127",
            Achievements = await GetAchievements(pikaGameId),
        };
    }

    private async Task<List<AchievementDbModel>> GetAchievements(Guid pikaGameId)
    {
        var achievements = await _steamClient.GetAchievements(AppId);
        return achievements.Select(a => new AchievementDbModel
        {
            GameId = pikaGameId,
            Name = a.DisplayName,
            SourceId = a.Name,
        }).ToList();
    }
}
