using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Common;

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    public static EntityDto FromDbModel(EntityDbModel model)
    {
        return new EntityDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
