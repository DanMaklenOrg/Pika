namespace Pika.Model;

public readonly record struct Entity(ResourceId Id, string Name, ResourceId Class)
{
    public List<ResourceId> Stats { get; init; } = [];

    public override string ToString() => Id;
}
