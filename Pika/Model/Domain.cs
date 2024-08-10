namespace Pika.Model;

public abstract record PikaResource(ResourceId Id, string Name)
{
    public override string ToString() => $"{Id} ({Name})";
}

public readonly record struct ResourceId(string Id)
{
    public override string ToString() => Id;

    public static ResourceId InduceFromName(string name) => IdUtilities.Normalize(name);

    public static implicit operator ResourceId(string id) => new(id);
    public static implicit operator string(ResourceId id) => id.ToString();
}

public record Domain(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public List<Class> Classes { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Project> Projects { get; init; } = [];
}

public record Project(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public required List<Objective> Objectives { get; init; }

    public override string ToString() => Name;
}

public record Objective(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public required List<Requirement> Requirements { get; init; }

    public readonly record struct Requirement(ResourceId Class)
    {
        public ResourceId Stat { get; init; } = "dummyId";
        public int Min { get; init; } = 0;
    }
}

public record Class(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public List<Stat> Stats { get; init; } = [];
}

public record Entity(ResourceId Id, string Name, ResourceId Class)
{
    public List<Stat> Stats { get; init; } = [];
}

public record Stat(ResourceId Id, string Name, Stat.StatType Type) : PikaResource(Id, Name)
{
    public int? Min { get; init; }
    public int? Max { get; init; }
    public List<string>? EnumValues { get; init; }

    public enum StatType
    {
        Boolean = 1,
        IntegerRange = 2,
        OrderedEnum = 3,
    }
}
