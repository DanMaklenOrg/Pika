using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public interface IUserStatsDao
{
    Task Create(UserStatsDbModel userStats);

    Task<UserStatsDbModel?> Get(string userId, string gameId);
}
