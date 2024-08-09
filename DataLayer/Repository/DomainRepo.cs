using Pika.DataLayer.Dao;
using Pika.DataLayer.Mapper;
using Pika.Model;
using Pika.Repository;

namespace Pika.DataLayer.Repository;

public class DomainRepo : IDomainRepo
{
    private readonly IDomainDao _domainDao;

    public DomainRepo(IDomainDao domainDao)
    {
        _domainDao = domainDao;
    }

    public async Task Create(Domain domain)
    {
        var dbModel = DbModelMapper.ToDbModel(domain);
        await _domainDao.Create(dbModel);
    }

    public async Task<Domain?> Get(string id)
    {
        var domain = await _domainDao.Get(id);
        return DbModelMapper.FromDbModel(domain);
    }

    public async Task<List<Domain>> GetAll()
    {
        var domains = await _domainDao.GetAll();
        return domains.ConvertAll(d => DbModelMapper.FromDbModel(d).Value);
    }
}
