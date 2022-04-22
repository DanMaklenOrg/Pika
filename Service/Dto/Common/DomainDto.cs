using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonConstructor]
    public DomainDto(string name, string? id = null)
    {
        this.Id = id ?? Guid.Empty.ToString("N");
        this.Name = name;
    }
}
