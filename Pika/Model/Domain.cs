namespace Pika.Model;

public record Domain
{
    public DomainId Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Stat> Stats { get; set; } = new List<Stat>();

    public List<Entity> Entities { get; set; } = new List<Entity>();

    public List<Domain> SubDomains { get; set; } = new List<Domain>();
}
