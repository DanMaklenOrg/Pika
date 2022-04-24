using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
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
    public async Task<ActionResult<DomainDto>> AddDomain(DomainDto domainDto, [FromHeader] string? authorization)
    {
        if (authorization != this.config.CurrentValue.Token) return this.Unauthorized();

        var domainModel = new DomainDbModel
        {
            Name = domainDto.Name,
        };

        await this.db.Domains.AddAsync(domainModel);
        await this.db.SaveChangesAsync();

        domainModel.RootEntry = new EntryDbModel
        {
            Domain = domainModel,
            Title = $"{domainDto.Name} Root Entry",
        };

        await this.db.SaveChangesAsync();
        return domainModel.Adapt<DomainDto>();
    }

    [HttpGet("{domainId}/profile")]
    public async Task<GetDomainProfileResponseDto> GetDomainProfile(string domainId)
    {
        await this.db.Entries.Where(entry => entry.Domain.Id == domainId.Adapt<Guid>()).ToListAsync();
        DomainDbModel domain = await this.db.Domains
            .Include(model => model.RootEntry)
            .Include(model => model.Projects)
            .ThenInclude(model => model.Objectives)
            .ThenInclude(model => model.Entries)
            .SingleAsync(model => model.Id == Guid.Parse(domainId));

        return new GetDomainProfileResponseDto
        {
            RootEntry = domain.RootEntry!.Adapt<EntryDto>(),
            Projects = domain.Projects.Adapt<List<ProjectDto>>(),
        };
    }

    [HttpPost("{domainId}/project")]
    public async Task<ActionResult> AddProjects(string domainId, [FromBody] List<ProjectDto> projects, [FromHeader] string? authorization)
    {
        if (authorization != this.config.CurrentValue.Token) return this.Unauthorized();

        var projectsDbModel = projects.Adapt<List<ProjectDbModel>>();

        this.db.AttachRange(projectsDbModel.SelectMany(proj => proj.Objectives).SelectMany(obj => obj.Entries));
        DomainDbModel domain = await this.db.Domains.SingleAsync(entry => entry.Id == Guid.Parse(domainId));
        domain.Projects.AddRange(projectsDbModel);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
