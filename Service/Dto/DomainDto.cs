using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDto> Stats { get; init; }

    [JsonPropertyName("classes")]
    public List<ClassDto> Classes { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDto> Entities { get; init; }

    [JsonPropertyName("projects")]
    public List<ProjectDto> Projects { get; init; }
}

public readonly struct ClassDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDto> Stats { get; init; }
}

public readonly struct ProjectDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("objectives")]
    public List<ObjectiveDto> Objectives { get; init; }
}

public readonly struct ObjectiveDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("requirements")]
    public List<ObjectiveRequirementDto> Requirements { get; init; }
}

public readonly struct ObjectiveRequirementDto
{
    [JsonPropertyName("class")]
    public string Class { get; init; }

    [JsonPropertyName("stat")]
    public string Stat { get; init; }

    [JsonPropertyName("min")]
    public int Min { get; init; }
}


public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<string> Stats { get; init; }

    [JsonPropertyName("class")]
    public string Class { get; init; }
}

public readonly struct StatDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("type")]
    public StatTypeEnumDto Type { get; init; }

    [JsonPropertyName("min")]
    public int? Min { get; init; }

    [JsonPropertyName("max")]
    public int? Max { get; init; }

    [JsonPropertyName("enum_values")]
    public List<string>? EnumValues { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatTypeEnumDto
{
    Boolean,
    IntegerRange,
    OrderedEnum,
}
