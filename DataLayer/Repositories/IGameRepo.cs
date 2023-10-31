using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public interface IGameRepo
{
    Task Create(GameDbModel game);

    Task<GameDbModel> Get(Guid id);

    Task<List<GameDbModel>> GetAll();
}
