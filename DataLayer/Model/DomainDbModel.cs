namespace Pika.DataLayer.Model;

public class DomainDbModel
{
    public Guid Id { get; set; }

    public string Name { get; init; } = null!;

    public EntryDbModel RootEntry { get; set; } = null!;
}
