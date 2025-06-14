using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class GameDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("steam_app_id")]
    public uint? SteamAppId { get; init; }

    [JsonPropertyName("achievements")]
    public List<AchievementDbModel>? Achievements { get; init; }

    [JsonPropertyName("categories")]
    public List<CategoryDbModel>? Categories { get; init; }

    [JsonPropertyName("tags")]
    public List<TagDbModel>? Tags { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDbModel>? Entities { get; init; }

    protected override void SetKeys()
    {
        PartitionKey = $"Game#{Id}";
        SortKey = "Game";
    }
}

public class AchievementDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("objectives")]
    public List<ObjectiveDbModel>? Objectives { get; init; }

    [JsonPropertyName("criteria_category")]
    public string? CriteriaCategory { get; init; }
}

public class ObjectiveDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("criteria_category")]
    public string? CriteriaCategory { get; init; }
}

public class CategoryDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

public class TagDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("category")]
    public required string Category { get; init; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; init; }
}
