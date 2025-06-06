using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class UserStatsDbModel : BaseDbModel
{
    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }

    [JsonPropertyName("game_id")]
    public required string GameId { get; init; }

    [JsonPropertyName("entity_stats")]
    public List<UserEntityStatDbModel> EntityStats { get; init; } = [];

    protected override void SetKeys()
    {
        PartitionKey = $"UserStat#{UserId}#{GameId}";
        SortKey = "UserStat";
    }
}

public class UserEntityStatDbModel
{
    [JsonPropertyName("entity_id")]
    public required string EntityId { get; init; }

    [JsonPropertyName("stat_id")]
    public required string StatId { get; init; }

    [JsonPropertyName("value")]
    public required string Value { get; init; }
}
