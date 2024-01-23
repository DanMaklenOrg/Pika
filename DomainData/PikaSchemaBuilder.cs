using Pika.DataLayer.Repositories;

namespace Pika.DomainData;

public class PikaSchemaBuilder
{
    private readonly IDomainRepo _domainRepo;
    private readonly IAchievementRepo _achievementRepo;

    public PikaSchemaBuilder(IDomainRepo domainRepo, IAchievementRepo achievementRepo)
    {
        _domainRepo = domainRepo;
        _achievementRepo = achievementRepo;
    }

    public async Task<GameSchema> BuildSchema(Guid gameId)
    {
        var game = await _domainRepo.Get(gameId.ToString());
        var achievements = await _achievementRepo.GetAll(gameId);

        return new GameSchema
        {
            Version = game.Version,
            Achievements = achievements,
        };
    }
}
