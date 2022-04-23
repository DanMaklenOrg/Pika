using System.Text.Json.Serialization;
using Pika.Service.Dto.Common;

namespace Pika.Service.Dto.Response;

public struct GetDomainProfileResponseDto
{
    [JsonPropertyName("root_entry")]
    public EntryDto RootEntry { get; set; }

    [JsonPropertyName("root_entry")]
    public List<ProjectDto> Projects { get; set; }
}
