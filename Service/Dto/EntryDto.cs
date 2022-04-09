using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct EntryDto
{
    [JsonPropertyName("title")]
    public string Title { get; init; }

    public List<EntryDto> Children { get; init; }

    public List<ObjectiveDto> Objectives { get; init; }
}
