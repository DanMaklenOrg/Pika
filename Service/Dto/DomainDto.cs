using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
