namespace Pika.Model;

public record Domain
{
    public DomainId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Stat> Stats { get; set; } = new();
    public List<Entity> Entities { get; set; } = new();
    public List<Project> Projects { get; set; } = new();
    public List<Domain> SubDomains { get; set; } = new();
}
