using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public interface IUserStatsDao
{
    Task Create(UserStatsDbModel domain);

    Task<UserStatsDbModel?> Get(string userId, string domainId);
}
