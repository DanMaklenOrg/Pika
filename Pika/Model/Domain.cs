namespace Pika;

public readonly struct Domain
{
    public DomainId Id { get; init; }
    public string Name { get; init; }
    public List<Stat> Stats { get; init; }
    public List<Entity> Entities { get; init; }
    private List<Domain> SubDomains { get; init; }
}
