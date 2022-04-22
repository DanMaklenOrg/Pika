using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/entry")]
public class EntryController : ControllerBase
{
    private readonly PikaDataContext db;
    private readonly IOptionsMonitor<ServiceConfig> config;

    public EntryController(PikaDataContext db, IOptionsMonitor<ServiceConfig> config)
    {
        this.db = db;
        this.config = config;
    }

    [HttpPost("{entryId}")]
    public async Task<ActionResult> AddChildEntries(string entryId, [FromBody] List<EntryDto> entries, [FromHeader] string? authorization)
    {
        if (authorization != this.config.CurrentValue.Token) return this.Unauthorized();

        var entriesDbModel = entries.Adapt<List<EntryDbModel>>();

        EntryDbModel parentEntry = await this.db.Entries.SingleAsync(entry => entry.Id == Guid.Parse(entryId));
        parentEntry.Children.AddRange(entriesDbModel);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
