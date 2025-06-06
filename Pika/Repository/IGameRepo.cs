using Pika.Model;

namespace Pika.Repository;

public interface IGameRepo
{
    Task Create(Game game);

    Task<Game?> Get(string id);

    Task<List<Game>> GetAll();
}
