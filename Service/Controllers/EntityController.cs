using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Response;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/entity")]
public class EntityController : ControllerBase
{
    private readonly PikaDataContext db;

    public EntityController(PikaDataContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Authorize]
    public async Task<List<EntityDto>> GetEntities(Guid domainId)
    {
        List<EntityDbModel> entities = await this.db.Entities.Where(entity => entity.Domain.Id == domainId).ToListAsync();
        return entities.ConvertAll(EntityDto.FromDbModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<EntityDto> AddEntity(Guid domainId, string name)
    {
        DomainDbModel domain = new DomainDbModel { Id = domainId };
        this.db.Attach(domain);

        EntityDbModel entity = new EntityDbModel
        {
            Name = name,
            Domain = domain,
        };
        await this.db.Entities.AddAsync(entity);
        await this.db.SaveChangesAsync();
        return EntityDto.FromDbModel(entity);
    }
}
