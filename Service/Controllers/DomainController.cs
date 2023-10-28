using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.DataLayer.Model;
using Pika.DataLayer.Repositories;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/domain")]
public class DomainController : ControllerBase
{
    private readonly IDomainRepo _domainRepo;

    public DomainController(IDomainRepo domainRepo)
    {
        _domainRepo = domainRepo;
    }

    [HttpGet("{domainId}")]
    public async Task<DomainDto> Get(Guid domainId)
    {
        var domain = await _domainRepo.Get(domainId);
        return DtoMapper.ToDto(domain);
    ;}


    [HttpGet("all")]
    public async Task<List<DomainDto>> GetAll()
    {
        var allDomains = await _domainRepo.GetAll();
        return allDomains.ConvertAll(DtoMapper.ToDto);
    }

    [HttpPost]
    [Authorize]
    public async Task<DomainDto> AddDomain(string name)
    {
        var domain = new DomainDbModel { Name = name };
        await _domainRepo.Create(domain);
        return DtoMapper.ToDto(domain);
    }
}
