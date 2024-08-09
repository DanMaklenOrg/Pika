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
            Stats = model.Stats.ConvertAll(ToDto),
            Tags = model.Tags.ConvertAll(ToDto),
            Classes = model.Classes.ConvertAll(ToDto),
        };
    }

    private static ClassDto ToDto(Class model)
    {
        return new ClassDto
        {
            Id = model.Id,
            Stats = model.Stats.ConvertAll<string>(s => s),
            Tags = model.Tags.ConvertAll<string>(t => t),
        };
    }

    private static TagDto ToDto(Tag model)
    {
        return new TagDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    private static ProjectDto ToDto(Project model)
    {
        return new ProjectDto
        {
            Title = model.Title,
            Objectives = model.Objectives.ConvertAll(ToDto),
        };
    }

    private static ObjectiveDto ToDto(Objective model)
    {
        return new ObjectiveDto
        {
            Title = model.Title,
            Requirements = model.Requirements.ConvertAll(ToDto),
        };
    }

    private static ObjectiveRequirementDto ToDto(ObjectiveRequirement model)
    {
        return new ObjectiveRequirementDto
        {
            Class = model.Class,
            Stat = model.Stat,
            Min = model.Min,
        };
    }


    private static EntityDto ToDto(Entity model)
    {
        return new EntityDto
        {
            Id = model.Id,
            Name = model.Name,
            Stats = model.Stats.ConvertAll<string>(s => s),
            Tags = model.Tags.ConvertAll<string>(t => t),
            Class = model.Class,
            Classes_Deprecated = [model.Class],
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
                StatType.Boolean => StatTypeEnumDto.Boolean,
                StatType.IntegerRange => StatTypeEnumDto.IntegerRange,
                StatType.OrderedEnum => StatTypeEnumDto.OrderedEnum,
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
