using System.Diagnostics.CodeAnalysis;

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

public record Game(ResourceId Id, string Name) : PikaResource(Id, Name)
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
    public List<Attribute> Attributes { get; init; } = [];
    public List<Stat> Stats { get; init; } = [];
}

public record Entity(ResourceId Id, string Name, ResourceId Class) : PikaResource(Id, Name)
{
    public List<Attribute> Attributes { get; init; } = [];
    public List<Stat> Stats { get; init; } = [];
}

public record Attribute(ResourceId Id, int Value);

public record Stat(ResourceId Id, string Name, Stat.StatType Type) : PikaResource(Id, Name)
{
    public IntOrAttribute? Min { get; init; }
    public IntOrAttribute? Max { get; init; }
    public List<string>? EnumValues { get; init; }

    public readonly struct IntOrAttribute
    {
        public int? ConstValue { get; init; }

        public ResourceId? AttributeId { get; init; }

        [MemberNotNullWhen(true, nameof(ConstValue))]
        public bool IsConstValue => ConstValue.HasValue;

        [MemberNotNullWhen(true, nameof(AttributeId))]
        public bool IsAttribute => AttributeId.HasValue;

        public static implicit operator IntOrAttribute(int constValue) => new() { ConstValue = constValue };
        public static implicit operator IntOrAttribute(ResourceId id) => new() { AttributeId = id };
    }

    public enum StatType
    {
        Boolean = 1,
        IntegerRange = 2,
        OrderedEnum = 3,
    }
}
