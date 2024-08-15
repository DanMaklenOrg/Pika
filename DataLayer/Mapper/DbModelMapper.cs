using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Pika.DataLayer.Model;
using Pika.Model;
using Attribute = Pika.Model.Attribute;

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
        };
    }

    private static ProjectDbModel ToDbModel(Project project)
    {
        return new ProjectDbModel
        {
            Id = project.Id,
            Name = project.Name,
            Objectives = project.Objectives.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveDbModel ToDbModel(Objective objective)
    {
        return new ObjectiveDbModel
        {
            Id = objective.Id,
            Name = objective.Name,
            Requirements = objective.Requirements.ConvertAll(r => new ObjectiveDbModel.RequirementDbModel()
            {
                Class = r.Class,
                Stat = r.Stat,
                Min = r.Min,
            }),
        };
    }

    private static ClassDbModel ToDbModel(Class model)
    {
        return new ClassDbModel
        {
            Id = model.Id,
            Name = model.Name,
            Attributes = model.Attributes.ConvertAll(ToDbModel),
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
            Attributes = entity.Attributes.ConvertAll(ToDbModel),
            Stats = entity.Stats.ConvertAll(ToDbModel),
        };
    }

    private static StatDbModel ToDbModel(Stat stat)
    {
        return new StatDbModel
        {
            Id = stat.Id,
            Name = stat.Name,
            Type = stat.Type.ToString(),
            Min = ToDbModel(stat.Min),
            Max = ToDbModel(stat.Max),
            EnumValues = stat.EnumValues,
        };
    }

    private static AttributeDbModel ToDbModel(Attribute attribute)
    {
        return new AttributeDbModel
        {
            Id = attribute.Id,
            Value = attribute.Value,
        };
    }

    [return: NotNullIfNotNull("intOrAttribute")]
    private static StatDbModel.IntOrAttributeDbModel? ToDbModel(Stat.IntOrAttribute? intOrAttribute)
    {
        if (!intOrAttribute.HasValue) return null;
        return new StatDbModel.IntOrAttributeDbModel
        {
            ConstValue = intOrAttribute.Value.ConstValue,
            AttributeId = intOrAttribute.Value.AttributeId,
        };
    }


    [return: NotNullIfNotNull("model")]
    public static Domain? FromDbModel(DomainDbModel? model)
    {
        if (model is null) return null;
        return new Domain(model.Id, model.Name)
        {
            Projects = model.Projects?.ConvertAll(FromDbModel) ?? [],
            Classes = model.Classes?.ConvertAll(FromDbModel) ?? [],
            Entities = model.Entities?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static Project FromDbModel(ProjectDbModel model)
    {
        return new Project(model.Id, model.Name)
        {
            Objectives = model.Objectives.ConvertAll(FromDbModel),
        };
    }

    private static Objective FromDbModel(ObjectiveDbModel model)
    {
        return new Objective(model.Id, model.Name)
        {
            Requirements = model.Requirements.ConvertAll(r => new Objective.Requirement
            {
                Class = r.Class,
                Stat = r.Stat,
                Min = r.Min,
            }),
        };
    }

    private static Class FromDbModel(ClassDbModel model)
    {
        return new Class(model.Id, model.Name)
        {
            Attributes = model.Attributes.ConvertAll(FromDbModel),
            Stats = model.Stats.ConvertAll(FromDbModel),
        };
    }

    private static Entity FromDbModel(EntityDbModel model)
    {
        return new Entity(model.Id, model.Name, model.Class)
        {
            Attributes = model.Attributes.ConvertAll(FromDbModel),
            Stats = model.Stats?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static Attribute FromDbModel(AttributeDbModel model)
    {
        return new Attribute(model.Id, model.Value);
    }

    private static Stat FromDbModel(StatDbModel model)
    {
        return new Stat(model.Id, model.Name, Enum.Parse<Stat.StatType>(model.Type))
        {
            Min = FromDbModel(model.Min),
            Max = FromDbModel(model.Max),
            EnumValues = model.EnumValues,
        };
    }

    [return: NotNullIfNotNull("model")]
    private static Stat.IntOrAttribute? FromDbModel(StatDbModel.IntOrAttributeDbModel? model)
    {
        if (model is null) return null;
        if (model.ConstValue is not null) return new Stat.IntOrAttribute { ConstValue = model.ConstValue };
        if (model.AttributeId is not null) return new Stat.IntOrAttribute { AttributeId = model.AttributeId };
        throw new UnreachableException();
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
