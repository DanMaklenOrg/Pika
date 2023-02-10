using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Table("Domain")]
public class DomainDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Name { get; init; } = default!;

    public EntryDbModel? RootEntry { get; set; }

    public List<ProjectDbModel> Projects { get; set; } = new();

    [InverseProperty(nameof(EntryDbModel.Domain))]
    public List<EntryDbModel> RelatedEntries { get; set; } = new();

    [InverseProperty(nameof(EntityDbModel.Domain))]
    public List<EntityDbModel> Entities { get; set; } = new();
}
