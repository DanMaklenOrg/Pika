using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
using Pika.Service.Dto.Response;
using Pika.Service.Utilities;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/domain")]
public class DomainController : ControllerBase
{
    private readonly PikaDataContext db;
    private readonly ILogger<DomainController> logger;

    public DomainController(PikaDataContext db, ILogger<DomainController> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    [HttpGet("test")]
    public async Task<List<DomainDto>> GetDomainListTest()
    {
        this.logger.LogInformation("Called GetDomainList");
        List<DomainDbModel> allDomains = new List<DomainDbModel>
        {
            new DomainDbModel
            {
                Id = Guid.NewGuid(),
                Achievements = new List<AchievementDbModel>(),
                Entities = new List<EntityDbModel>(),
                Name = "Test Domain",
                Projects = new List<ProjectDbModel>(),
                RelatedEntries = new List<EntryDbModel>(),
                RootEntry = null,
            }
        };
        this.logger.LogInformation("got {DomainsCount} domains", allDomains.Count);
        return allDomains.ConvertAll(DomainDto.FromDbModel);
    }

    [HttpGet]
    public async Task<List<DomainDto>> GetDomainList()
    {
        this.logger.LogInformation("Called GetDomainList");
        List<DomainDbModel> allDomains = await this.db.Domains.ToListAsync();
        this.logger.LogInformation("got {DomainsCount} domains", allDomains.Count);
        return allDomains.ConvertAll(DomainDto.FromDbModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<DomainDto> AddDomain(string domainName)
    {
        var domainModel = new DomainDbModel
        {
            Name = domainName,
        };

        await this.db.Domains.AddAsync(domainModel);
        await this.db.SaveChangesAsync();

        domainModel.RootEntry = new EntryDbModel
        {
            Domain = domainModel,
            Title = $"{domainName} Root Entry",
        };

        await this.db.SaveChangesAsync();
        return DomainDto.FromDbModel(domainModel);
    }

    [HttpGet("{domainId}/profile")]
    [Authorize]
    public async Task<GetDomainProfileResponseDto> GetDomainProfile(string domainId)
    {
        await this.db.Entries.Where(entry => entry.Domain.Id == domainId.Adapt<Guid>()).ToListAsync();
        DomainDbModel domain = await this.db.Domains
            .Include(model => model.RootEntry)
            .Include(model => model.Projects)
            .ThenInclude(model => model.Objectives)
            .ThenInclude(model => model.Targets)
            .SingleAsync(model => model.Id == Guid.Parse(domainId));

        foreach (EntryDbModel entry in Traverse.Dfs(domain.RootEntry!, node => node.Children))
            entry.Children = entry.Children.OrderBy(child => child.Title).ToList();

        List<ProgressDbModel> progress = await this.db.Progress.Where(model => model.Objective.Project.Domain.Id == domainId.Adapt<Guid>()).ToListAsync();

        return new GetDomainProfileResponseDto
        {
            RootEntry = domain.RootEntry!.Adapt<EntryDto>(),
            Projects = domain.Projects.Adapt<List<ProjectDto>>(),
            Progress = progress.Adapt<List<ProgressDto>>(),
        };
    }

    [HttpPost("{domainId}/project")]
    [Authorize]
    public async Task AddProjects(string domainId, [FromBody] List<ProjectDto> projects)
    {
        var projectsDbModel = projects.Adapt<List<ProjectDbModel>>();

        this.db.AttachRange(projectsDbModel.SelectMany(proj => proj.Objectives).SelectMany(obj => obj.Targets));
        DomainDbModel domain = await this.db.Domains.SingleAsync(entry => entry.Id == Guid.Parse(domainId));
        domain.Projects.AddRange(projectsDbModel);
        await this.db.SaveChangesAsync();
    }
}
