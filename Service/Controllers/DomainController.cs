using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.DataLayer.Model;
using Pika.DataLayer.Repositories;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("domain")]
public class DomainController : ControllerBase
{
    private readonly IDomainRepo _domainRepo;

    public DomainController(IDomainRepo domainRepo)
    {
        _domainRepo = domainRepo;
    }

    [HttpGet("{domainId}")]
    public async Task<DomainDto> Get(string domainId)
    {
        var domain = await _domainRepo.Get(domainId);
        if (domain is null) throw new Exception("Domain Not Found");
        return DtoMapper.ToDto(domain);
    }


    [HttpGet("all")]
    public async Task<List<DomainSummaryDto>> GetAll()
    {
        var allDomains = await _domainRepo.GetAll();
        return allDomains.ConvertAll(DtoMapper.ToSummaryDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<DomainSummaryDto> Add(string name, string id, string? version)
    {
        if (IdUtilities.IsValidId(id)) throw new ArgumentException("Invalid Id Format", nameof(id));
        // TODO: resolve race condition here
        if (await _domainRepo.Get(id) != null) throw new Exception("Id already exist");
        var domain = new DomainDbModel
        {
            Id = id,
            Name = name,
            Version = version ?? "0.0.0",
        };
        await _domainRepo.Create(domain);
        return DtoMapper.ToSummaryDto(domain);
    }
}
