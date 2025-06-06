using Pika.DataLayer.Dao;
using Pika.DataLayer.Mapper;
using Pika.Model;
using Pika.Repository;

namespace Pika.DataLayer.Repository;

public class GameRepo : IGameRepo
{
    private readonly IGameDao _gameDao;

    public GameRepo(IGameDao gameDao)
    {
        _gameDao = gameDao;
    }

    public async Task Create(Game game)
    {
        var dbModel = DbModelMapper.ToDbModel(game);
        await _gameDao.Create(dbModel);
    }

    public async Task<Game?> Get(string id)
    {
        var game = await _gameDao.Get(id);
        return DbModelMapper.FromDbModel(game);
    }

    public async Task<List<Game>> GetAll()
    {
        var gamess = await _gameDao.GetAll();
        return gamess.ConvertAll(DbModelMapper.FromDbModel)!;
    }
}
