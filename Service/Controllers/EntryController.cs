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

        EntryDbModel parentEntry = await this.db.Entries.Include(entry => entry.Domain)
            .SingleAsync(entry => entry.Id == Guid.Parse(entryId));

        TypeAdapterConfig adapterConfig = TypeAdapterConfig.GlobalSettings
            .Fork(c => c.ForType<EntryDto, EntryDbModel>().AfterMapping(model => model.Domain = parentEntry.Domain));

        var entriesDbModel = entries.Adapt<List<EntryDbModel>>(adapterConfig);

        parentEntry.Children.AddRange(entriesDbModel);
        await this.db.SaveChangesAsync();

        return this.Ok();
    }
}
