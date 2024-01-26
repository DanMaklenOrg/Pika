using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Model;
using Pika.Repository;
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
}
