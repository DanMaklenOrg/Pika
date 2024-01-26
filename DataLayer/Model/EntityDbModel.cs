using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("stats")]
    public List<string> Stats { get; init; } = new();
}
