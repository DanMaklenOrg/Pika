using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct ObjectiveDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("required_count")]
    public int RequiredCount { get; init; }

    [JsonPropertyName("entries_id")]
    public List<string> EntriesId { get; init; }
}
