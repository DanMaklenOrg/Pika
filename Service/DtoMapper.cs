using Pika.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static DomainSummaryDto ToSummaryDto(Domain model)
    {
        return new DomainSummaryDto
        {
            Id = model.Id.FullyQualifiedId,
            Name = model.Name,
        };
    }

    public static DomainDto ToDto(Domain model)
    {
        return new DomainDto
        {
            Id = model.Id.FullyQualifiedId,
            Name = model.Name,
            SubDomains = model.SubDomains.ConvertAll(ToDto),
            Entities = model.Entities.ConvertAll(ToDto),
            Stats = model.Stats.ConvertAll(ToDto),
        };
    }

    private static EntityDto ToDto(Entity model)
    {
        return new EntityDto
        {
            Id = model.Id.FullyQualifiedId,
            Name = model.Name,
            Stats = model.Stats.ConvertAll(x => x.FullyQualifiedId),
        };
    }

    private static StatDto ToDto(Stat model)
    {
        return new StatDto
        {
            Id = model.Id.FullyQualifiedId,
            Name = model.Name,
            Type = model.Type switch
            {
                StatType.Boolean => StatTypeEnumDto.Boolean,
                StatType.IntegerRange => StatTypeEnumDto.IntegerRange,
                _ => throw new ArgumentOutOfRangeException()
            },
            Min = model.Min,
            Max = model.Max,
        };
    }
}
