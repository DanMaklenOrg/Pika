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
}
