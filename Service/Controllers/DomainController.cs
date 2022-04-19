using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
using Pika.Service.Dto.Request;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/domain")]
public class DomainController : ControllerBase
{
    private readonly PikaDataContext db;

    public DomainController(PikaDataContext db)
    {
        this.db = db;
    }

    [HttpGet]
    public async Task<List<DomainDto>> GetDomainList()
    {
        List<DomainDbModel> allDomains = await this.db.Domains.ToListAsync();
        return allDomains.Adapt<List<DomainDto>>();
    }

    [HttpPost]
    public async Task<DomainDto> AddDomain(AddDomainRequestDto requestDto)
    {
        var model = new DomainDbModel
        {
            Name = requestDto.Name,
            RootEntry = new EntryDbModel
            {
                Title = $"{requestDto.Name} Project",
            },
        };

        await this.db.Domains.AddAsync(model);
        await this.db.SaveChangesAsync();
        return model.Adapt<DomainDto>();
    }

    [HttpGet("{domainId}/profile")]
    public async Task<List<EntryDto>> GetDomainProfile(string domainId)
    {
        var entriesDto = new List<EntryDto>();

        DomainDbModel domain = await this.db.Domains
            .Include(model => model.RootEntry)
            .ThenInclude(model => model.Objectives)
            .SingleAsync(model => model.Id == Guid.Parse(domainId));
        var queue = new Queue<EntryDbModel>();
        queue.Enqueue(domain.RootEntry);
        while (queue.Count > 0)
        {
            EntryDbModel entry = queue.Dequeue();

            foreach (EntryDbModel childModel in entry.Objectives.SelectMany(objective => objective.RequiredEntries)) queue.Enqueue(childModel);
            entriesDto.Add(entry.Adapt<EntryDto>());
        }

        return entriesDto;
    }
}
