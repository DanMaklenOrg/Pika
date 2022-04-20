using System.ComponentModel.DataAnnotations;

namespace Pika.DataLayer.Model;

public class DomainDbModel
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; init; } = default!;

    public EntryDbModel RootEntry { get; set; } = default!;

    public List<ProjectDbModel> Projects { get; set; } = new();
}
