using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

public class ObjectiveDbModel
{
    [Key]
    public Guid Id { get; set; }

    public ProjectDbModel Project { get; set; } = new();

    [Unicode(false)]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    public List<EntryDbModel> Targets { get; set; } = new();

    public int RequiredCount { get; set; }
}
