using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct DomainProfileDto
{
    [JsonPropertyName("entries")]
    public List<string> RootEntriesId { get; init; }

    [JsonPropertyName("entries")]
    public List<EntryDto> Entries { get; init; }
}
