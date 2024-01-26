using Pika.Model;

namespace Pika.Repository;

public interface IDomainRepo
{
    Task Create(Domain domain);

    Task<Domain?> Get(string id);

    Task<List<Domain>> GetAll();
}
