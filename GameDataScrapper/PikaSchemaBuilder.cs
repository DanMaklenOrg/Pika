using Pika.DataLayer.Repositories;

namespace Pika.GameDataScrapper;

public class PikaSchemaBuilder
{
    private readonly IGameRepo _gameRepo;
    private readonly IAchievementRepo _achievementRepo;

    public PikaSchemaBuilder(IGameRepo gameRepo, IAchievementRepo achievementRepo)
    {
        _gameRepo = gameRepo;
        _achievementRepo = achievementRepo;
    }

    public async Task<GameSchema> BuildSchema(Guid gameId)
    {
        var game = await _gameRepo.Get(gameId);
        var achievements = await _achievementRepo.GetAll(gameId);

        return new GameSchema
        {
            Version = game.Version,
            Achievements = achievements,
        };
    }
}
