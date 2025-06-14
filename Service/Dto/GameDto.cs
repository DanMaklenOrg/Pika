using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct GameDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("achievements")]
    public List<AchievementDto>? Achievements { get; init; }

    [JsonPropertyName("categories")]
    public List<CategoryDto>? Categories { get; init; }

    [JsonPropertyName("tags")]
    public List<TagDto>? Tags { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDto>? Entities { get; init; }
}

public readonly struct AchievementDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("objectives")]
    public List<ObjectiveDto>? Objectives { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("criteria_category")]
    public string? CriteriaCategory { get; init; }
}

public readonly struct ObjectiveDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("criteria_category")]
    public string? CriteriaCategory { get; init; }
}

public readonly struct CategoryDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}

public readonly struct TagDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("category")]
    public string Category { get; init; }

    [JsonPropertyName("tags")]
    public List<string>? Tags { get; init; }
}
