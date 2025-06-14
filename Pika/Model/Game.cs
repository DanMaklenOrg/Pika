namespace Pika.Model;

public abstract record PikaResource(ResourceId Id, string Name)
{
    public override string ToString() => $"{Id} ({Name})";
}

public readonly record struct ResourceId(string Id)
{
    public override string ToString() => Id;

    public static implicit operator ResourceId(string id) => new(id);
    public static implicit operator string(ResourceId id) => id.ToString();
}

public record Game(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public uint? SteamAppId { get; set; }

    public List<Category> Categories { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Achievement> Achievements { get; init; } = [];
    public List<Tag> Tags { get; init; } = [];
}

public record Achievement(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public string? Description { get; init; } = string.Empty;
    public List<Objective> Objectives { get; init; } = [];
    public ResourceId? CriteriaCategory { get; init; }
}

public record Objective(ResourceId Id, string Name) : PikaResource(Id, Name)
{
    public string? Description { get; init; } = string.Empty;
    public ResourceId? CriteriaCategory { get; init; }
}

public record Category(ResourceId Id, string Name) : PikaResource(Id, Name);

public record Tag(ResourceId Id, string Name) : PikaResource(Id, Name);

public record Entity(ResourceId Id, string Name, ResourceId Category) : PikaResource(Id, Name)
{
    public List<ResourceId> Tags { get; init; } = [];
}
