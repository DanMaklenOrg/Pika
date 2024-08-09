namespace Pika.Model;

public readonly record struct Class(ResourceId Id)
{
    public List<Stat> Stats { get; init; } = [];
    public List<ResourceId> StatsIds { get; init; } = [];
    public List<ResourceId> Tags { get; init; } = [];
}
