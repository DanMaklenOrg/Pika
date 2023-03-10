using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Table("Tag")]
public class TagDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    [InverseProperty(nameof(EntityDbModel.Tags))]
    public List<EntityDbModel> Entities { get; set; } = new();

    public List<ActionDbModel> Actions { get; set; } = new();
}
