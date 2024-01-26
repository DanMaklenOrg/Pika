using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("sub_domains")]
    public List<DomainDto> SubDomains { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDto> Stats { get; init; }

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
    public List<string> Stats {get; init; }
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
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StatTypeEnumDto
{
    Boolean,
    IntegerRange,
}
