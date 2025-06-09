using Pika.Model;

namespace Pika.Repository;

public interface IGameProgressRepo
{
    Task Create(GameProgress gameProgress);

    Task<GameProgress?> Get(string userId, ResourceId gameId);
}
