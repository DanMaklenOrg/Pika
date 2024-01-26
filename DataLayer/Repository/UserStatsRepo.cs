using Pika.DataLayer.Dao;
using Pika.DataLayer.Mapper;
using Pika.Model;
using Pika.Repository;

namespace Pika.DataLayer.Repository;

public class UserStatsRepo : IUserStatsRepo
{
    private readonly IUserStatsDao _userStatsDao;

    public UserStatsRepo(IUserStatsDao userStatsDao)
    {
        _userStatsDao = userStatsDao;
    }

    public async Task Create(UserStats uerStats)
    {
        var dbModel = DbModelMapper.ToDbModel(uerStats);
        await _userStatsDao.Create(dbModel);
    }

    public async Task<UserStats> Get(string userId, DomainId domainId)
    {
        var userStat = await _userStatsDao.Get(userId, domainId.FullyQualifiedId);
        return DbModelMapper.FromDbModel(userStat) ?? new UserStats
        {
            UserId = userId,
            DomainId = domainId,
            EntityStats = new List<UserEntityStat>(),
        };
    }
}
