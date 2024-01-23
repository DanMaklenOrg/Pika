using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("version")]
    public string Version { get; init; } = default!;

    public override void SetKeys()
    {
        PartitionKey = $"Domain#{Id}";
        SortKey = "Domain";
    }
}
