using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct DomainProfileDto
{
    [JsonPropertyName("project_list")]
    public List<EntryDto> ProjectList { get; init; }
}
