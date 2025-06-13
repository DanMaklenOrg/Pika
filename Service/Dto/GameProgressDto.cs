using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct GameProgressDto
{
    [JsonPropertyName("user_id")]
    public string UserId { get; init; }

    [JsonPropertyName("game")]
    public string Game { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("achievement_progress")]
    public List<AchievementProgressDto> AchievementProgress { get; init; }
}

public readonly struct AchievementProgressDto
{
    [JsonPropertyName("achievement")]
    public string Achievement { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("objective_progress")]
    public List<ObjectiveProgressDto> ObjectiveProgress { get; init; }

    [JsonPropertyName("entities_done")]
    public List<string> EntitiesDone { get; init; }
}

public readonly record struct ObjectiveProgressDto
{
    [JsonPropertyName("objective")]
    public string Objective { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("entities_done")]
    public List<string> EntitiesDone { get; init; }
}
