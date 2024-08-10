using Pika.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static DomainSummaryDto ToSummaryDto(Domain model)
    {
        return new DomainSummaryDto
        {
            Id = model.Id.ToString(),
            Name = model.Name,
        };
    }

    public static DomainDto ToDto(Domain model)
    {
        return new DomainDto
        {
            Id = model.Id,
            Name = model.Name,
            Entities = model.Entities.ConvertAll(ToDto),
            Projects = model.Projects.ConvertAll(ToDto),
            Classes = model.Classes.ConvertAll(ToDto),
        };
    }

    private static ClassDto ToDto(Class model)
    {
        return new ClassDto
        {
            Id = model.Id,
            Name = model.Name,
            Stats = model.Stats.ConvertAll(ToDto),
        };
    }

    private static ProjectDto ToDto(Project model)
    {
        return new ProjectDto
        {
            Id = model.Id,
            Name = model.Name,
            Objectives = model.Objectives.ConvertAll(ToDto),
        };
    }

    private static ObjectiveDto ToDto(Objective model)
    {
        return new ObjectiveDto
        {
            Id = model.Id,
            Name = model.Name,
            Requirements = model.Requirements.ConvertAll(r => new ObjectiveDto.RequirementDto()
            {
                Class = r.Class,
                Stat = r.Stat,
                Min = r.Min,
            }),
        };
    }

    private static EntityDto ToDto(Entity model)
    {
        return new EntityDto
        {
            Id = model.Id,
            Name = model.Name,
            Stats = model.Stats.ConvertAll(ToDto),
            Class = model.Class,
        };
    }

    private static StatDto ToDto(Stat model)
    {
        return new StatDto
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type switch
            {
                Stat.StatType.Boolean => StatDto.StatTypeEnumDto.Boolean,
                Stat.StatType.IntegerRange => StatDto.StatTypeEnumDto.IntegerRange,
                Stat.StatType.OrderedEnum => StatDto.StatTypeEnumDto.OrderedEnum,
                _ => throw new ArgumentOutOfRangeException()
            },
            Min = model.Min,
            Max = model.Max,
            EnumValues = model.EnumValues,
        };
    }

    public static UserStatsDto ToDto(UserStats userStats)
    {
        return new UserStatsDto
        {
            EntityStats = userStats.EntityStats.ConvertAll(ToDto),
        };
    }

    private static UserEntityStatDto ToDto(UserEntityStat entityStat)
    {
        return new UserEntityStatDto
        {
            EntityId = entityStat.EntityId,
            StatId = entityStat.StatId,
            Value = entityStat.Value,
        };
    }

    public static UserStats FromDto(UserStatsDto statsDto, string userId, string domainId)
    {
        return new UserStats
        {
            UserId = userId,
            DomainId = domainId,
            EntityStats = statsDto.EntityStats.ConvertAll(FromDbModel),
        };
    }

    private static UserEntityStat FromDbModel(UserEntityStatDto entityStatDto)
    {
        return new UserEntityStat
        {
            EntityId = entityStatDto.EntityId,
            StatId = entityStatDto.StatId,
            Value = entityStatDto.Value,
        };
    }
}
