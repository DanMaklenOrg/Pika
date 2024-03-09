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
            Classes = domain.Classes.ConvertAll(ToDbModel),
            Stats = domain.Stats.ConvertAll(ToDbModel),
            SubDomains = domain.SubDomains.ConvertAll(ToDbModel),
        };
    }

    private static ProjectDbModel ToDbModel(Project project)
    {
        return new ProjectDbModel
        {
            Id = project.Id.FullyQualifiedId,
            Name = project.Name,
        };
    }

    private static ClassDbModel ToDbModel(Class model)
    {
        return new ClassDbModel
        {
            Id = model.Id.FullyQualifiedId,
            Stats = model.Stats.ConvertAll(c => c.FullyQualifiedId),
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

    [return: NotNullIfNotNull("domain")]
    public static Domain? FromDbModel(DomainDbModel? domain)
    {
        if (domain is null) return null;
        return new Domain
        {
            Id = domain.Id,
            Name = domain.Name,
            Projects = domain.Projects?.ConvertAll(FromDbModel) ?? [],
            Entities = domain.Entities?.ConvertAll(FromDbModel) ?? [],
            Stats = domain.Stats?.ConvertAll(FromDbModel) ?? [],
            SubDomains = (domain.SubDomains?.ConvertAll(FromDbModel) ?? [])!,
        };
    }

    private static Project FromDbModel(ProjectDbModel entity)
    {
        return new Project()
        {
            Id = entity.Id,
            Name = entity.Name,
        };
    }

    private static Entity FromDbModel(EntityDbModel entity)
    {
        return new Entity
        {
            Id = entity.Id,
            Name = entity.Name,
            Classes = entity.Classes?.ConvertAll(ResourceId.ParseResourceId) ?? [],
            Stats = entity.Stats?.ConvertAll(ResourceId.ParseResourceId) ?? [],
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
