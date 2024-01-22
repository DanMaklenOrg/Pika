namespace Pika.Model;

public struct DomainId
{
    public string Id { get; set; }
    public List<string> SubDomainIds { get; set; }
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
