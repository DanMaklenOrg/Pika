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
            Id = domain.Id.FullyQualifiedId,
            Name = domain.Name,
            Entities = domain.Entities.ConvertAll(ToDbModel),
            Projects = domain.Projects.ConvertAll(ToDbModel),
            Tags = domain.Tags.ConvertAll(ToDbModel),
            Classes = domain.Classes.ConvertAll(ToDbModel),
            Stats = domain.Stats.ConvertAll(ToDbModel),
        };
    }

    private static ProjectDbModel ToDbModel(Project project)
    {
        return new ProjectDbModel
        {
            Id = project.Id.FullyQualifiedId,
            Title = project.Title,
            Objectives = project.Objectives.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveDbModel ToDbModel(Objective project)
    {
        return new ObjectiveDbModel
        {
            Title = project.Title,
            Requirements = project.Requirements.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveRequirementDbModel ToDbModel(ObjectiveRequirement project)
    {
        return new ObjectiveRequirementDbModel
        {
            Class = project.Class.FullyQualifiedId,
            Stat = project.Stat.FullyQualifiedId,
            Min = project.Min,
        };
    }

    private static ClassDbModel ToDbModel(Class model)
    {
        return new ClassDbModel
        {
            Id = model.Id.FullyQualifiedId,
            Stats = model.Stats.ConvertAll(s => s.FullyQualifiedId),
            Tags = model.Tags.ConvertAll(t => t.FullyQualifiedId),
        };
    }

    private static TagDbModel ToDbModel(Tag model)
    {
        return new TagDbModel
        {
            Id = model.Id.FullyQualifiedId,
            Name = model.Name,
        };
    }

    private static EntityDbModel ToDbModel(Entity entity)
    {
        return new EntityDbModel
        {
            Id = entity.Id.FullyQualifiedId,
            Name = entity.Name,
            Classes = entity.Classes.ConvertAll(c => c.FullyQualifiedId),
            Stats = entity.Stats.ConvertAll(s => s.FullyQualifiedId),
            Tags = entity.Tags.ConvertAll(t => t.FullyQualifiedId),
        };
    }

    private static StatDbModel ToDbModel(Stat stat)
    {
        return new StatDbModel
        {
            Id = stat.Id.FullyQualifiedId,
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
            Tags = model.Tags?.ConvertAll(FromDbModel) ?? [],
            Classes = model.Classes?.ConvertAll(FromDbModel) ?? [],
            Entities = model.Entities?.ConvertAll(FromDbModel) ?? [],
            Stats = model.Stats?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static Project FromDbModel(ProjectDbModel model)
    {
        return new Project
        {
            Id = model.Id,
            Title = model.Title,
            Objectives = model.Objectives.ConvertAll(FromDbModel),
        };
    }

    private static Objective FromDbModel(ObjectiveDbModel model)
    {
        return new Objective
        {
            Title = model.Title,
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

    private static Tag FromDbModel(TagDbModel model)
    {
        return new Tag
        {
            Id = model.Id,
            Name = model.Name,
        };
    }

    private static Class FromDbModel(ClassDbModel model)
    {
        return new Class
        {
            Id = model.Id,
            Stats = model.Stats?.ConvertAll(ResourceId.ParseResourceId) ?? [],
            Tags = model.Tags?.ConvertAll(ResourceId.ParseResourceId) ?? [],
        };
    }
    private static Entity FromDbModel(EntityDbModel model)
    {
        return new Entity
        {
            Id = model.Id,
            Name = model.Name,
            Tags = model.Tags?.ConvertAll(ResourceId.ParseResourceId) ?? [],
            Classes = model.Classes?.ConvertAll(ResourceId.ParseResourceId) ?? [],
            Stats = model.Stats?.ConvertAll(ResourceId.ParseResourceId) ?? [],
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
            DomainId = userStats.DomainId.FullyQualifiedId,
            EntityStats = userStats.EntityStats.ConvertAll(ToDbModel),
            CompletedProjectIds = userStats.CompletedProjectIds.ConvertAll(pid => pid.FullyQualifiedId),
        };
    }

    private static UserEntityStatDbModel ToDbModel(UserEntityStat userEntityStat)
    {
        return new UserEntityStatDbModel
        {
            EntityId = userEntityStat.EntityId.FullyQualifiedId,
            StatId = userEntityStat.StatId.FullyQualifiedId,
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
            CompletedProjectIds = userStats.CompletedProjectIds.ConvertAll(ResourceId.ParseResourceId),
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
