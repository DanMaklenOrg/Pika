namespace Pika.Model;

public readonly record struct Entity(ResourceId Id, string Name)
{
    public List<ResourceId> Classes { get; init; } = [];
    public List<ResourceId> Stats { get; init; } = [];
    public List<ResourceId> Tags { get; init; } = [];

    public override string ToString() => Id.FullyQualifiedId;
}
