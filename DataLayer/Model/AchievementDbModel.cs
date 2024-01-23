using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class AchievementDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [JsonPropertyName("domainId")]
    public Guid DomainId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("sourceId")]
    public string SourceId { get; init; } = default!;

    public override void SetKeys()
    {
        PartitionKey = $"Domain#{DomainId}";
        SortKey = $"Achievement#{Id}";
    }
}
