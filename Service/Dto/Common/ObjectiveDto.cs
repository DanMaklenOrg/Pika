using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct ObjectiveDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("type")]
    public ObjectiveTypeDto Type { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("required_count")]
    public int RequiredCount { get; init; }

    [JsonPropertyName("required_entries_id")]
    public List<string> RequiredEntriesId { get; init; }
}
