using System.Text.RegularExpressions;

namespace Pika;

public readonly struct ResourceId
{
    public string Id { get; init; }
    public DomainId Domain { get; init; }
    public string FullyQualifiedId => $"{Domain}/{Id}";

    public ResourceId(string id, DomainId domain)
    {
        Id = id;
        Domain = domain;
    }

    public override string ToString() => FullyQualifiedId;

    public static ResourceId ParseResourceId(string id)
    {
        var segments = id.Split('/');
        return segments.Length switch
        {
            2 => new ResourceId(segments[1], segments[0]),
            _ => throw new ArgumentException("Id must be of form {{domain}}/{{id}} or {{id}}"),
        };
    }

    public static implicit operator ResourceId(string id) => ParseResourceId(id);
}
