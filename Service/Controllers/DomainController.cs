using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
using Pika.Service.Dto.Request;
using Pika.Service.Dto.Response;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/domain")]
public class DomainController : ControllerBase
{
    private readonly PikaDataContext db;
    private readonly IOptionsMonitor<ServiceConfig> config;

    public DomainController(PikaDataContext db, IOptionsMonitor<ServiceConfig> config)
    {
        this.db = db;
        this.config = config;
    }

    [HttpGet]
    public async Task<List<DomainDto>> GetDomainList()
    {
        List<DomainDbModel> allDomains = await this.db.Domains.ToListAsync();
        return allDomains.Adapt<List<DomainDto>>();
    }

    [HttpPost]
    public async Task<ActionResult<DomainDto>> AddDomain(AddDomainRequestDto requestDto, [FromHeader] string? authorization)
    {
        if (authorization != this.config.CurrentValue.Token) return this.Unauthorized();

        var model = new DomainDbModel
        {
            Name = requestDto.Name,
            RootEntry = new EntryDbModel
            {
                Title = $"{requestDto.Name} Root Entry",
            },
        };

        await this.db.Domains.AddAsync(model);
        await this.db.SaveChangesAsync();
        return model.Adapt<DomainDto>();
    }

    [HttpGet("{domainId}/profile")]
    public async Task<GetDomainProfileResponseDto> GetDomainProfile(string domainId)
    {
        DomainDbModel domain = await this.db.Domains
            .Include(model => model.RootEntry)
            .Include(model => model.Projects)
            .SingleAsync(model => model.Id == Guid.Parse(domainId));

        return new GetDomainProfileResponseDto
        {
            RootEntry = domain.RootEntry.Adapt<EntryDto>(),
            Projects = domain.Projects.Adapt<List<ProjectDto>>(),
        };
    }
}
