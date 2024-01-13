namespace Pika.Model;

public readonly struct DomainId
{
    public string Id { get; init; }
    public List<string> SubDomainIds { get; init; }
    public string FullyQualifiedId => SubDomainIds.Count > 0 ? $"{string.Join('.', SubDomainIds)}.{Id}" : Id;

    public override string ToString() => FullyQualifiedId;

    public static implicit operator DomainId(string id) => ParseDomainId(id);

    public static DomainId ParseDomainId(string id)
    {
        var segments = id.Split('.');
        return new DomainId
        {
            Id = segments.Last(),
            SubDomainIds = segments.SkipLast(1).ToList(),
        };
    }
}
