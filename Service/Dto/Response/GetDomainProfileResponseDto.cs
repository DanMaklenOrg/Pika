using System.Text.Json.Serialization;
using Pika.Service.Dto.Common;

namespace Pika.Service.Dto.Response;

public struct GetDomainProfileResponseDto
{
    [JsonPropertyName("root_entry")]
    public EntryDto RootEntry { get; set; }

    [JsonPropertyName("projects")]
    public List<ProjectDto> Projects { get; set; }

    [JsonPropertyName("progress")]
    public List<ProgressDto> Progress { get; set; }
}
