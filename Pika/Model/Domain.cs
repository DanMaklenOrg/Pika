namespace Pika.Model;

public record Domain
{
    public DomainId Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public List<Stat> Stats { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Project> Projects { get; init; } = [];
    public List<Domain> SubDomains { get; init; } = [];
}
