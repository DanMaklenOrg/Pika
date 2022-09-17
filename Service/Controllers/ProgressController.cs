using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/progress")]
public class ProgressController : ControllerBase
{
    private readonly PikaDataContext db;

    public ProgressController(PikaDataContext db)
    {
        this.db = db;
    }

    [HttpPut]
    [Authorize]
    public async Task UpdateProgress(ProgressDto dto)
    {
        ObjectiveTargetDbModel objectiveTarget = await this.db.ObjectiveTargets
            .Include(model => model.Objective)
            .Include(model => model.Target)
            .SingleAsync(model =>
                model.Objective.Id == dto.ObjectiveId.Adapt<Guid>() &&
                model.Target.Id == dto.TargetId.Adapt<Guid>());

        if (dto.Progress < 0 || dto.Progress > objectiveTarget.Objective.RequiredCount)
            throw new Exception("Progress Amount out of range!");

        var progressModel = new ProgressDbModel
        {
            UserId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value.Adapt<Guid>(),
            Objective = objectiveTarget.Objective,
            Target = objectiveTarget.Target,
            Progress = dto.Progress,
        };

        bool exist = this.db.Progress.Any(model =>
            model.UserId == progressModel.UserId &&
            model.Objective.Id == progressModel.Objective.Id &&
            model.Target.Id == progressModel.Target.Id);
        if (!exist) await this.db.Progress.AddAsync(progressModel);
        else this.db.Progress.Update(progressModel);
        await this.db.SaveChangesAsync();
    }
}
