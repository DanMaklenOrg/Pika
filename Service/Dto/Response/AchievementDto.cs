using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Response;

public readonly struct AchievementDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    public static AchievementDto FromDbModel(AchievementDbModel model)
    {
        return new AchievementDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
