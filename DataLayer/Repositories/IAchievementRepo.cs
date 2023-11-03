using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public interface IAchievementRepo
{
    public Task Create(AchievementDbModel achievement);
    public Task<List<AchievementDbModel>> GetAll(Guid gameId);
}
