using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class GameProgressDbModel : BaseDbModel
{
    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }

    [JsonPropertyName("game")]
    public required string Game { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("achievement_progress")]
    public List<AchievementProgressDbModel>? AchievementProgress { get; init; }

    protected override void SetKeys()
    {
        PartitionKey = $"Game#{Game}#Progress#{UserId}";
        SortKey = "GameProgress";
    }
}

public class AchievementProgressDbModel
{
    [JsonPropertyName("achievement")]
    public required string Achievement { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("objective_progress")]
    public List<ObjectiveProgressDbModel>? ObjectiveProgress { get; init; }

    [JsonPropertyName("entities_done")]
    public List<string>? EntitiesDone { get; init; }
}

public class ObjectiveProgressDbModel
{
    [JsonPropertyName("objective")]
    public required string Objective { get; init; }

    [JsonPropertyName("completed")]
    public bool Completed { get; init; }

    [JsonPropertyName("entities_done")]
    public List<string>? EntitiesDone { get; init; }
}
