using Pika.DataLayer.Dao;
using Pika.DataLayer.Mapper;
using Pika.Model;
using Pika.Repository;

namespace Pika.DataLayer.Repository;

public class GameProgressRepo(IGameProgressDao gameProgressDao) : IGameProgressRepo
{
    public async Task Create(GameProgress gameProgress)
    {
        var dbModel = DbModelMapper.ToDbModel(gameProgress);
        await gameProgressDao.Create(dbModel);
    }

    public async Task<GameProgress?> Get(string userId, ResourceId gameId)
    {
        var gameProgress = await gameProgressDao.Get(userId, gameId);
        return DbModelMapper.FromDbModel(gameProgress);
    }
}
