using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("version")]
    public string Version { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDto> Entities { get; init; }
}

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<EntityStatsDto> Stats {get; init; }
}

public readonly struct EntityStatsDto
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
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatTypeEnumDto
{
    Boolean = 1,
    IntegerRange = 2,
}
