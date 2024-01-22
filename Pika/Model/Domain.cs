namespace Pika.Model;

public struct Domain
{
    public DomainId Id { get; set; }
    public string Name { get; set; }
    public List<Stat> Stats { get; set; }
    public List<Entity> Entities { get; set; }
    private List<Domain> SubDomains { get; set; }
}
