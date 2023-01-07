using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Common;

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("parent")]
    public Guid? ParentId { get; init; }

    [JsonPropertyName("children")]
    public List<Guid> Children { get; init; }

    public static EntityDto FromDbModel(EntityDbModel model)
    {
        return new EntityDto
        {
            Id = model.Id,
            Name = model.Name,
            ParentId = model.Parent?.Id,
            Children = model.Children.ConvertAll(child => child.Id),
        };
    }
}
