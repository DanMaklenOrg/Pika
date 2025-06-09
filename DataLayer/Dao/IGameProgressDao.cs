using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public interface IGameProgressDao
{
    Task Create(GameProgressDbModel gameProgress);

    Task<GameProgressDbModel?> Get(string userId, string gameId);
}
