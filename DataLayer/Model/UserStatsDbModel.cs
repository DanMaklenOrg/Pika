using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class UserStatsDbModel : BaseDbModel
{
    [JsonPropertyName("user_id")]
    public required string UserId { get; init; }

    [JsonPropertyName("domain_id")]
    public required string DomainId { get; init; }

    [JsonPropertyName("entity_stats")]
    public List<UserEntityStatDbModel> EntityStats { get; init; } = [];

    [JsonPropertyName("completed_project_ids")]
    public List<string> CompletedProjectIds { get; init; } = [];

    protected override void SetKeys()
    {
        PartitionKey = $"UserStat#{UserId}#{DomainId}";
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
