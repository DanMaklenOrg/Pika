namespace Pika.Model;

public struct Stat
{
    public ResourceId Id { get; set; }
    public string Name { get; set; }
    public StatType Type { get; set; }
    public int? Min { get; set; }
    public int? Max { get; set; }
}

public enum StatType
{
    Boolean = 1,
    IntegerRange = 2,
}
