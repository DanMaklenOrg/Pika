using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

public class EntryDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    public EntryDbModel? Parent { get; set; }

    public List<EntryDbModel> Children { get; set; } = new();

    public List<ObjectiveDbModel> Objectives { get; set; } = new();
}
