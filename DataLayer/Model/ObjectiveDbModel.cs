using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

public class ObjectiveDbModel
{
    [Key]
    public Guid Id { get; set; }

    public ObjectiveType Type { get; set; }

    [InverseProperty("Objectives")]
    public EntryDbModel Entry { get; set; } = null!;

    [Unicode(false)]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    public int RequiredCount { get; set; }

    public List<EntryDbModel> RequiredEntries { get; set; } = new();
}
