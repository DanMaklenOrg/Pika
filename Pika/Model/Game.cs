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
    public List<Tag> Tags { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Achievement> Achievements { get; init; } = [];
}

public record Achievement(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public string Description { get; init; } = string.Empty;
    public List<Objective> Objectives { get; init; } = [];
}

public record Objective(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public string Description { get; init; } = string.Empty;
    public Tag? Criteria { get; init; }
}

public record Tag(ResourceId Id, string Name) : PikaResource(Id, Name);

public record Entity(ResourceId Id, string Name, ResourceId Tag) : PikaResource(Id, Name);
