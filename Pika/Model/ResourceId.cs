namespace Pika.Model;

public readonly record struct ResourceId(string Id, DomainId Domain)
{
    public string FullyQualifiedId => $"{Domain}/{Id}";

    public override string ToString() => FullyQualifiedId;

    public static ResourceId ParseResourceId(string id)
    {
        var segments = id.Split('/');
        return new ResourceId(segments[1], segments[0]);
    }

    public static ResourceId InduceFromName(string resourceName, DomainId domainId)
    {
        return new ResourceId(IdUtilities.Normalize(resourceName), domainId);
    }

    public static implicit operator ResourceId(string id) => ParseResourceId(id);
}
