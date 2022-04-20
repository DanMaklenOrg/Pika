using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct EntryDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("children")]
    public List<EntryDto> Children { get; init; }
}
