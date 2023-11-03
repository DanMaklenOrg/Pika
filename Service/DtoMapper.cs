using Pika.DataLayer.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static GameDto ToDto(GameDbModel model)
    {
        return new GameDto
        {
            Id = model.Id,
            Name = model.Name,
            Version = model.Version,
        };
    }
}
