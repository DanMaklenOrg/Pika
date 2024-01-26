using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class UserStatsDbModel : BaseDbModel
{
    [JsonPropertyName("user_id")]
    public string UserId { get; init; } = default!;

    [JsonPropertyName("domain_id")]
    public string DomainId { get; init; } = default!;

    public List<UserEntityStatDbModel> EntityStats { get; init; } = new();

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
    public int Value { get; init; }
}
