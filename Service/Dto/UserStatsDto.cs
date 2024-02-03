using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct UserStatsDto
{
    [JsonPropertyName("entity_stats")]
    public List<UserEntityStatDto> EntityStats { get; init; }

    [JsonPropertyName("completed_project_ids")]
    public List<string> CompletedProjectIds { get; init; }
}

public readonly struct UserEntityStatDto
{
    [JsonPropertyName("entity_id")]
    public string EntityId { get; init; }

    [JsonPropertyName("stat_id")]
    public string StatId { get; init; }

    [JsonPropertyName("value")]
    public string Value { get; init; }
}
