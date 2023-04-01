using System.Security.Claims;
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

    [HttpPut("{id:guid}/unlock")]
    [Authorize]
    public async Task UnlockAchievement([FromRoute] Guid id)
    {
        Guid userId = new Guid(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        // await this.db.UserAchievements.AnyAsync(userAchievement => userAchievement.Achievement.Id == id && userAchievement.UserId == userId);

        UserAchievementDbModel userAchievement = new UserAchievementDbModel
        {
            UserId = userId,
            AchievementId = id,
            UnlockState = true,
        };
        if (await this.db.UserAchievements.ContainsAsync(userAchievement))
            this.db.UserAchievements.Update(userAchievement);
        else
            await this.db.UserAchievements.AddAsync(userAchievement);
        await this.db.SaveChangesAsync();
    }


    [HttpGet("unlocked")]
    [Authorize]
    public async Task<List<AchievementDto>> GetUnlockedAchievements([FromQuery] Guid domainId)
    {
        Guid userId = new Guid(this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var unlockedAchievements = await this.db.UserAchievements
            .Where(model => model.UserId == userId && model.Achievement.Domain.Id == domainId && model.UnlockState)
            .Select(model => model.Achievement)
            .ToListAsync();

        return unlockedAchievements.ConvertAll(AchievementDto.FromDbModel);
    }
}
