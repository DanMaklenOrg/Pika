using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pika.DataLayer.Model;

public class DomainDbModel
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; init; } = default!;

    public EntryDbModel RootEntry { get; set; } = default!;

    public List<ProjectDbModel> Projects { get; set; } = new();

    [InverseProperty("Domain")]
    public List<EntryDbModel> RelatedEntries { get; set; } = new();
}
