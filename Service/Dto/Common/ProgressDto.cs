using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public struct ProgressDto
{
    [JsonPropertyName("objective_id")]
    public string ObjectiveId { get; set; }

    [JsonPropertyName("target_id")]
    public string TargetId { get; set; }

    [JsonPropertyName("progress")]
    public int Progress { get; set; }
}
