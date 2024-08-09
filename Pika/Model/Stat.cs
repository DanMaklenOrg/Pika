namespace Pika.Model;

public readonly record struct Stat(ResourceId Id, string Name, StatType Type)
{
    public int? Min { get; init; }
    public int? Max { get; init; }
    public List<string>? EnumValues { get; init; }

    public override string ToString() => Id.FullyQualifiedId;
}

public enum StatType
{
    Boolean = 1,
    IntegerRange = 2,
    OrderedEnum = 3,
}
