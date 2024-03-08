using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pika.Model;
using Pika.Repository;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("domain")]
public class DomainController : ControllerBase
{
    private readonly IDomainRepo _domainRepo;
    private readonly IUserStatsRepo _userStatsRepo;

    public DomainController(IDomainRepo domainRepo, IUserStatsRepo userStatsRepo)
    {
        _domainRepo = domainRepo;
        _userStatsRepo = userStatsRepo;
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

    [Authorize]
    [HttpGet("{domainId}/stats")]
    public async Task<UserStatsDto> GetStats(string domainId)
    {
        var userId = this.User.Identity!.Name!;
        var stats = await _userStatsRepo.Get(userId, domainId);
        return DtoMapper.ToDto(stats ?? new UserStats(userId, domainId));
    }

    [Authorize]
    [HttpPost("{domainId}/stats")]
    public async Task SetStats(string domainId, UserStatsDto statsDto)
    {
        var userId = this.User.Identity!.Name!;
        var stats = DtoMapper.FromDto(statsDto, userId, domainId);
        await _userStatsRepo.Create(stats);
    }
}
