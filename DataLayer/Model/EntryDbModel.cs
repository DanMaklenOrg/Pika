using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pika.DataLayer.Model;

public class EntryDbModel
{
    [Key]
    public Guid Id { get; set; }

    [Unicode(false)]
    [MaxLength(100)]
    public string Title { get; set; } = null!;

    public List<ObjectiveDbModel> Objectives { get; set; } = new();

    [InverseProperty("RequiredEntries")]
    public List<ObjectiveDbModel> ParentObjectives { get; set; } = null!;
}