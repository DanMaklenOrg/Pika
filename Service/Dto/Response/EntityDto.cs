using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Response;

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("tags")]
    public List<TagDto> Tags { get; init; }

    public static EntityDto FromDbModel(EntityDbModel model)
    {
        return new EntityDto
        {
            Id = model.Id,
            Name = model.Name,
            Tags = model.Tags.ConvertAll(TagDto.FromDbModel),
        };
    }
}
