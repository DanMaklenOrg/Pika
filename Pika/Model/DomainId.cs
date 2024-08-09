namespace Pika.Model;

public readonly record struct DomainId
{
    public string Id { get; init; }
    public string? SubDomainId { get; init; }
    public bool IsSubDomainId => !string.IsNullOrWhiteSpace(SubDomainId);
    public string FullyQualifiedId => IsSubDomainId ? $"{SubDomainId}.{Id}" : Id;

    public override string ToString() => FullyQualifiedId;

    public static implicit operator DomainId(string id) => ParseDomainId(id);

    public static DomainId ParseDomainId(string id)
    {
        var segments = id.Split('.');
        return new DomainId
        {
            Id = segments.Last(),
            SubDomainId = string.Join('.', segments.SkipLast(1)),
        };
    }
}
