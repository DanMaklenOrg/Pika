using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
