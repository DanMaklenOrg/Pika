namespace Pika.Model;

public readonly struct Entity
{
    public required ResourceId Id { get; init; }
    public required string Name { get; init; }
    public required ResourceId Class { get; init; }
    public List<ResourceId> Stats { get; init; } = [];
    public List<ResourceId> Tags { get; init; } = [];

    public Entity()
    {
    }

    public override string ToString() => Id;
}
