using System.Text.Json.Serialization;
using Pika.DataLayer.Model;

namespace Pika.Service.Dto.Response;

public readonly struct DomainDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    public static DomainDto FromDbModel(DomainDbModel model)
    {
        return new DomainDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
