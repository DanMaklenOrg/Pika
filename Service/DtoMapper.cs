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
            Entities = Enumerable.Range(0, 100).Select(i => new EntityDto
            {
                Id = $"entity{i}",
                Name = $"e{i}",
                Stats = new List<EntityStatsDto>
                {
                    new()
                    {
                        Id = "stat1",
                        Name = "Boolean Stat",
                        Type = StatTypeEnumDto.Boolean,
                    },
                    new()
                    {
                        Id = "stat2",
                        Name = "Integer Range Stat",
                        Type = StatTypeEnumDto.IntegerRange,
                        Min = 0,
                        Max = 10,
                    },
                }
            }).ToList(),
        };
    }
}
