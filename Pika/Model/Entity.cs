namespace Pika.Model;

public readonly struct Entity
{
    public required ResourceId Class { get; init; }
    public List<ResourceId> Stats { get; init; } = [];
    public List<ResourceId> Tags { get; init; } = [];

    public Entity()
    {
    }

    public override string ToString() => Id;
}
