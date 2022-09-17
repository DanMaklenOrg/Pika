using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Index(nameof(UserId))]
public class ProgressDbModel
{
    public Guid UserId { get; set; }

    public ObjectiveDbModel Objective { get; set; } = default!;

    public EntryDbModel Target { get; set; } = default!;

    public int Progress { get; set; }
}
