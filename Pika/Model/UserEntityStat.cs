namespace Pika.Model;

public readonly struct UserEntityStat
{
    public ResourceId EntityId { get; init; }

    public ResourceId StatId { get; init; }

    public string Value { get; init; }
}
