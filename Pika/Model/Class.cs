namespace Pika.Model;

public readonly record struct Class(ResourceId Id)
{
    public List<ResourceId> Stats { get; init; } = [];
    public List<ResourceId> Tags { get; init; } = [];
}
