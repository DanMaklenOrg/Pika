namespace Pika.Model;

public struct DomainId
{
    public string Id { get; init; }
    public string FullyQualifiedId => Id;

    public override string ToString() => FullyQualifiedId;

    public static implicit operator DomainId(string id) => ParseDomainId(id);

    public static DomainId ParseDomainId(string id)
    {
        return new DomainId
        {
            Id = id,
        };
    }
}
