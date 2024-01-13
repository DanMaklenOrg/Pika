namespace Pika;

public readonly struct Stat
{
    public ResourceId Id { get; init; }
    public string Name { get; init; }
    public StatType Type { get; init; }
    public int? Min { get; init; }
    public int? Max { get; init; }
}

public enum StatType
{
    Boolean = 1,
    IntegerRange = 2,
}
