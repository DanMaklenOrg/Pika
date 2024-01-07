using Pika.DataLayer.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static GameSummaryDto ToSummaryDto(GameDbModel model)
    {
        return new GameSummaryDto
        {
            Id = model.Id,
            Name = model.Name,
            Version = model.Version,
        };
    }

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
