using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

[Table("Achievement")]
public class AchievementDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Name { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    [Unicode(false)]
    [MaxLength(500)]
    public string Description { get; set; } = default!;

    public Uri? Image { get; set; }
}
