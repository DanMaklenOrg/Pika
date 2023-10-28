using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public interface IDomainRepo
{
    Task Create(DomainDbModel domain);

    Task<DomainDbModel> Get(Guid id);

    Task<List<DomainDbModel>> GetAll();
}
