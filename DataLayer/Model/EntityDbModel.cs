using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Table("Entity")]
public class EntityDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    public EntityDbModel? Parent { get; set; }

    [InverseProperty(nameof(EntityDbModel.Parent))]
    public List<EntityDbModel> Children { get; set; } = new();
}
