using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/objective")]
public class ObjectiveController : ControllerBase
{
    private readonly PikaDataContext db;
    private readonly IOptionsMonitor<ServiceConfig> config;

    public ObjectiveController(PikaDataContext db, IOptionsMonitor<ServiceConfig> config)
    {
        this.db = db;
        this.config = config;
    }

    [HttpPost("{objectiveId}/entry")]
    [Authorize]
    public async Task<ActionResult> AddEntries(string objectiveId, [FromBody] List<string> entriesId)
    {
        var entries = entriesId.Adapt<List<EntryDbModel>>();
        this.db.AttachRange(entries);
        ObjectiveDbModel objective = await this.db.Objectives.SingleAsync(obj => obj.Id == objectiveId.Adapt<Guid>());
        objective.Entries.AddRange(entries);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
