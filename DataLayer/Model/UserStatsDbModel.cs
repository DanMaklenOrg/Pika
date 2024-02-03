using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class UserStatsDbModel : BaseDbModel
{
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = default!;

    [JsonPropertyName("domain_id")]
    public string DomainId { get; init; } = default!;

    [JsonPropertyName("entity_stats")]
    public List<UserEntityStatDbModel> EntityStats { get; init; } = new();

    [JsonPropertyName("completed_project_ids")]
    public List<string> CompletedProjectIds { get; init; } = new();

    protected override void SetKeys()
    {
        PartitionKey = $"UserStat#{UserId}#{DomainId}";
        SortKey = "UserStat";
    }
}

public class UserEntityStatDbModel
{
    [JsonPropertyName("entity_id")]
    public string EntityId { get; init; } = default!;

    [JsonPropertyName("stat_id")]
    public string StatId { get; init; } = default!;

    [JsonPropertyName("value")]
    public string Value { get; init; } = default!;
}
