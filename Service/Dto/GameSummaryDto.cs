using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct GameSummaryDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
