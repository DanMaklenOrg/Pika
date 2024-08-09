namespace Pika.Model;

public readonly record struct Class(ResourceId Id)
{
    public List<Stat> Stats { get; init; } = [];
}
