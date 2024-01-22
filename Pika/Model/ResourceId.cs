namespace Pika.Model;

public struct ResourceId
{
    public string Id { get; set; }
    public DomainId Domain { get; set; }
    public string FullyQualifiedId => $"{Domain}/{Id}";

    public ResourceId(string id, DomainId domain)
    {
        Id = id;
        Domain = domain;
    }

    public override string ToString() => FullyQualifiedId;

    public static ResourceId ParseResourceId(string id, DomainId? scope = null)
    {
        var segments = id.Split('/');
        return segments.Length switch
        {
            1 when scope is not null => new ResourceId(segments[0], scope.Value),
            2 => new ResourceId(segments[1], segments[0]),
            _ => throw new ArgumentException("Id must be of form {{domain}}/{{id}} or {{id}}"),
        };
    }

    public static implicit operator ResourceId(string id) => ParseResourceId(id);
}
