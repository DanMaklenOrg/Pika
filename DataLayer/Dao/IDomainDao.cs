using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public interface IDomainDao
{
    Task Create(DomainDbModel domain);

    Task<DomainDbModel?> Get(string id);

    Task<List<DomainDbModel>> GetAll();
}
