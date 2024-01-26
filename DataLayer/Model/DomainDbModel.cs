using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("stats")]
    public List<StatDbModel> Stats { get; init; } = new();

    [JsonPropertyName("entries")]
    public List<EntityDbModel> Entities { get; init; } = new();

    [JsonPropertyName("subDomains")]
    public List<DomainDbModel> SubDomains { get; init; } = new();

    protected override void SetKeys()
    {
        PartitionKey = $"Domain#{Id}";
        SortKey = "Domain";
    }
}
