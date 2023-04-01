using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
using Pika.Service.Dto.Response;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/achievement")]
public class AchievementController : ControllerBase
{
    private readonly PikaDataContext db;

    public AchievementController(PikaDataContext db)
    {
        this.db = db;
    }

    [HttpGet]
    [Authorize]
    public async Task<List<AchievementDto>> GetAchievements(Guid domainId)
    {
        List<AchievementDbModel> achievements = await this.db.Achievements.Where(achievement => achievement.Domain.Id == domainId).ToListAsync();
        return achievements.ConvertAll(AchievementDto.FromDbModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<AchievementDto> AddAchievement(Guid domainId, AddAchievementRequestDto requestDto)
    {
        DomainDbModel domain = new DomainDbModel { Id = domainId };
        this.db.Attach(domain);

        AchievementDbModel achievement = new AchievementDbModel
        {
            Name = requestDto.Name,
            Domain = domain,
            Description = requestDto.Description,
            Image = requestDto.Image,
        };
        await this.db.Achievements.AddAsync(achievement);
        await this.db.SaveChangesAsync();
        return AchievementDto.FromDbModel(achievement);
    }
}
