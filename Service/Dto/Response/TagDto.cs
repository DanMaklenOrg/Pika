using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Response;

public readonly struct TagDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    public static TagDto FromDbModel(TagDbModel model)
    {
        return new TagDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
