using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

public class ProjectDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Title { get; set; } = default!;

    public DomainDbModel Domain { get; set; } = default!;

    public List<ObjectiveDbModel> Objectives { get; set; } = new();
}
