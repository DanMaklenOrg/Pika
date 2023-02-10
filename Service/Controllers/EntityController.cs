using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;

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
    public async Task<List<EntityDto>> GetDomainEntities(Guid domainId)
    {
        List<EntityDbModel> entities = await this.db.Entities.Where(entity => entity.Domain.Id == domainId).ToListAsync();
        return entities.ConvertAll(EntityDto.FromDbModel);
    }

    [HttpPost]
    public async Task<EntityDto> AddEntity(Guid domainId, string name, Guid? parentId = null)
    {
        DomainDbModel domain = new DomainDbModel { Id = domainId };
        this.db.Attach(domain);

        EntityDbModel? parent = null;
        if (parentId != null)
        {
            parent = new EntityDbModel { Id = parentId.Value };
            this.db.Attach(parent);
        }

        EntityDbModel entity = new EntityDbModel
        {
            Name = name,
            Domain = domain,
            Parent = parent,
        };
        await this.db.Entities.AddAsync(entity);
        await this.db.SaveChangesAsync();
        return EntityDto.FromDbModel(entity);
    }
}
