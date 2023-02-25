using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/tag")]
public class TagController : ControllerBase
{
    private readonly PikaDataContext db;

    public TagController(PikaDataContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<List<TagDto>> GetDomainTags(Guid domainId)
    {
        List<TagDbModel> tags = await this.db.Tags.Where(tag => tag.Domain.Id == domainId).ToListAsync();
        return tags.ConvertAll(TagDto.FromDbModel);
    }

    [HttpPost]
    public async Task<TagDto> AddTag(Guid domainId, string name)
    {
        DomainDbModel domain = new DomainDbModel { Id = domainId };
        this.db.Attach(domain);

        TagDbModel tag = new TagDbModel
        {
            Name = name,
            Domain = domain,
        };
        await this.db.Tags.AddAsync(tag);
        await this.db.SaveChangesAsync();
        return TagDto.FromDbModel(tag);
    }
}
