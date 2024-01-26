using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class StatDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; init; } = default!;

    [JsonPropertyName("min")]
    public int? Min { get; init; }

    [JsonPropertyName("max")]
    public int? Max { get; init; }
}
