using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public interface IGameDao
{
    Task Create(GameDbModel game);

    Task<GameDbModel?> Get(string id);

    Task<List<GameDbModel>> GetAll();
}
