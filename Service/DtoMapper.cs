using System.Diagnostics.CodeAnalysis;
using Pika.Model;
using Pika.Service.Dto;
using Attribute = Pika.Model.Attribute;

namespace Pika.Service;

public static class DtoMapper
{
    public static GameSummaryDto ToSummaryDto(Game model)
    {
        return new GameSummaryDto
        {
            Id = model.Id.ToString(),
            Name = model.Name,
        };
    }

    public static GameDto ToDto(Game model)
    {
        return new GameDto
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
            Attributes = model.Attributes.ConvertAll(ToDto),
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
            Attributes = model.Attributes.ConvertAll(ToDto),
            Stats = model.Stats.ConvertAll(ToDto),
            Class = model.Class,
        };
    }

    private static AttributeDto ToDto(Attribute model)
    {
        return new AttributeDto
        {
            Id = model.Id,
            Value = model.Value,
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
            Min = ToDto(model.Min),
            Max = ToDto(model.Max),
            EnumValues = model.EnumValues,
        };
    }

    [return: NotNullIfNotNull(nameof(intOrAttribute))]
    private static StatDto.IntOrAttributeDto? ToDto(Stat.IntOrAttribute? intOrAttribute)
    {
        if (!intOrAttribute.HasValue) return null;
        return new StatDto.IntOrAttributeDto
        {
            ConstValue = intOrAttribute.Value.ConstValue,
            AttributeId = intOrAttribute.Value.AttributeId
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

    public static UserStats FromDto(UserStatsDto statsDto, string userId, string GameId)
    {
        return new UserStats
        {
            UserId = userId,
            GameId = GameId,
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
