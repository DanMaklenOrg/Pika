using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/project")]
public class ProjectController : ControllerBase
{
    private readonly PikaDataContext db;
    private readonly IOptionsMonitor<ServiceConfig> config;

    public ProjectController(PikaDataContext db, IOptionsMonitor<ServiceConfig> config)
    {
        this.db = db;
        this.config = config;
    }

    [HttpPost("{projectId}/objective")]
    [Authorize]
    public async Task<ActionResult> AddObjectives(string projectId, [FromBody] List<ObjectiveDto> objectives)
    {
        var objectiveDbModels = objectives.Adapt<List<ObjectiveDbModel>>();

        this.db.AttachRange(objectiveDbModels.SelectMany(obj => obj.Entries));

        ProjectDbModel project = await this.db.Projects.SingleAsync(proj => proj.Id == projectId.Adapt<Guid>());
        project.Objectives.AddRange(objectiveDbModels);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
