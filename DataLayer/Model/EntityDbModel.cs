using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class EntityDbModel
{
    [JsonPropertyName("subDomains")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("subDomains")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("subDomains")]
    public List<string> Stats { get; init; } = new();
}
