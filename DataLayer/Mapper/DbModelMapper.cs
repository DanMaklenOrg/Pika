using System.Diagnostics.CodeAnalysis;
using Pika.DataLayer.Model;
using Pika.Model;

namespace Pika.DataLayer.Mapper;

public static class DbModelMapper
{
    public static DomainDbModel ToDbModel(Domain domain)
    {
        return new DomainDbModel
        {
            Id = domain.Id,
            Name = domain.Name,
            Entities = domain.Entities.ConvertAll(ToDbModel),
            Projects = domain.Projects.ConvertAll(ToDbModel),
            Classes = domain.Classes.ConvertAll(ToDbModel),
            Stats = domain.Stats.ConvertAll(ToDbModel),
        };
    }

    private static ProjectDbModel ToDbModel(Project project)
    {
        return new ProjectDbModel
        {
            Name = project.Name,
            Objectives = project.Objectives.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveDbModel ToDbModel(Objective project)
    {
        return new ObjectiveDbModel
        {
            Name = project.Name,
            Requirements = project.Requirements.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveRequirementDbModel ToDbModel(ObjectiveRequirement project)
    {
        return new ObjectiveRequirementDbModel
        {
            Class = project.Class,
            Stat = project.Stat,
            Min = project.Min,
        };
    }

    private static ClassDbModel ToDbModel(Class model)
    {
        return new ClassDbModel
        {
            Id = model.Id,
            Stats = model.Stats.ConvertAll(ToDbModel),
        };
    }

    private static EntityDbModel ToDbModel(Entity entity)
    {
        return new EntityDbModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Class = entity.Class,
            Stats = entity.Stats.ConvertAll<string>(s => s),
        };
    }

    private static StatDbModel ToDbModel(Stat stat)
    {
        return new StatDbModel
        {
            Id = stat.Id,
            Name = stat.Name,
            Type = stat.Type.ToString(),
            Min = stat.Min,
            Max = stat.Max,
            EnumValues = stat.EnumValues,
        };
    }

    [return: NotNullIfNotNull("model")]
    public static Domain? FromDbModel(DomainDbModel? model)
    {
        if (model is null) return null;
        return new Domain
        {
            Id = model.Id,
            Name = model.Name,
            Projects = model.Projects?.ConvertAll(FromDbModel) ?? [],
            Classes = model.Classes?.ConvertAll(FromDbModel) ?? [],
            Entities = model.Entities?.ConvertAll(FromDbModel) ?? [],
            Stats = model.Stats?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static Project FromDbModel(ProjectDbModel model)
    {
        return new Project
        {
            Name = model.Name,
            Objectives = model.Objectives.ConvertAll(FromDbModel),
        };
    }

    private static Objective FromDbModel(ObjectiveDbModel model)
    {
        return new Objective
        {
            Name = model.Name,
            Requirements = model.Requirements.ConvertAll(FromDbModel),
        };
    }

    private static ObjectiveRequirement FromDbModel(ObjectiveRequirementDbModel model)
    {
        return new ObjectiveRequirement
        {
            Class = model.Class,
            Stat = model.Stat,
            Min = model.Min,
        };
    }

    private static Class FromDbModel(ClassDbModel model)
    {
        return new Class
        {
            Id = model.Id,
            Stats = model.Stats.ConvertAll(FromDbModel),
        };
    }
    private static Entity FromDbModel(EntityDbModel model)
    {
        return new Entity
        {
            Id = model.Id,
            Name = model.Name,
            Class = model.Class,
            Stats = model.Stats?.ConvertAll<ResourceId>(s => s) ?? [],
        };
    }

    private static Stat FromDbModel(StatDbModel stat)
    {
        return new Stat
        {
            Id = stat.Id,
            Name = stat.Name,
            Type = Enum.Parse<StatType>(stat.Type),
            Min = stat.Min,
            Max = stat.Max,
            EnumValues = stat.EnumValues,
        };
    }

    public static UserStatsDbModel ToDbModel(UserStats userStats)
    {
        return new UserStatsDbModel
        {
            UserId = userStats.UserId,
            DomainId = userStats.DomainId,
            EntityStats = userStats.EntityStats.ConvertAll(ToDbModel),
        };
    }

    private static UserEntityStatDbModel ToDbModel(UserEntityStat userEntityStat)
    {
        return new UserEntityStatDbModel
        {
            EntityId = userEntityStat.EntityId,
            StatId = userEntityStat.StatId,
            Value = userEntityStat.Value,
        };
    }

    [return: NotNullIfNotNull("userStats")]
    public static UserStats? FromDbModel(UserStatsDbModel? userStats)
    {
        if (userStats is null) return null;
        return new UserStats
        {
            UserId = userStats.UserId,
            DomainId = userStats.DomainId,
            EntityStats = userStats.EntityStats.ConvertAll(FromDbModel),
        };
    }

    private static UserEntityStat FromDbModel(UserEntityStatDbModel userEntityStatDbModel)
    {
        return new UserEntityStat
        {
            EntityId = userEntityStatDbModel.EntityId,
            StatId = userEntityStatDbModel.StatId,
            Value = userEntityStatDbModel.Value,
        };
    }
}
