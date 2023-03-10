using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Table("Action")]
public class ActionDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    [InverseProperty(nameof(TagDbModel.Actions))]
    public List<TagDbModel> Tags { get; set; } = new();
}
