using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public interface IGameRepo
{
    Task Create(GameDbModel game);

    Task<GameDbModel?> Get(string id);

    Task<List<GameDbModel>> GetAll();
}
