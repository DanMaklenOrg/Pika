namespace Pika.Model;

public readonly record struct ResourceId(string Id)
{
    public override string ToString() => Id;

    public static ResourceId InduceFromName(string resourceName)
    {
        return IdUtilities.Normalize(resourceName);
    }

    public static implicit operator ResourceId(string id) => new(id);
    public static implicit operator string(ResourceId id) => id.ToString();
}
