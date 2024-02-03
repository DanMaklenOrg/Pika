using Pika.Model;

namespace Pika.Repository;

public interface IUserStatsRepo
{
    Task Create(UserStats uerStats);

    Task<UserStats?> Get(string userId, DomainId domainId);
}
