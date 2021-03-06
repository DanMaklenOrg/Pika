using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service.Dto.Common;
using Pika.Service.Utilities;

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

        EntryDbModel parentEntry = await this.db.Entries.Include(entry => entry.Domain)
            .SingleAsync(entry => entry.Id == Guid.Parse(entryId));

        var entriesDbModel = entries.Adapt<List<EntryDbModel>>();

        foreach (EntryDbModel model in entriesDbModel.SelectMany(entry => Traverse.Dfs(entry, node => node.Children))) model.Domain = parentEntry.Domain;

        parentEntry.Children.AddRange(entriesDbModel);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
