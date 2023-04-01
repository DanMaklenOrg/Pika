using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public readonly struct AddAchievementRequestDto
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("image")]
    public Uri? Image { get; init; }
}
