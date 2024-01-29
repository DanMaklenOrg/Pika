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
            Stats = domain.Stats.ConvertAll(ToDbModel),
            SubDomains = domain.SubDomains.ConvertAll(ToDbModel),
        };
    }

    private static EntityDbModel ToDbModel(Entity entity)
    {
        return new EntityDbModel
        {
            Id = entity.Id.FullyQualifiedId,
            Name = entity.Name,
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
            Entities = domain.Entities.ConvertAll(FromDbModel),
            Stats = domain.Stats.ConvertAll(FromDbModel),
            SubDomains = domain.SubDomains.ConvertAll(FromDbModel)!,
        };
    }

    private static Entity FromDbModel(EntityDbModel entity)
    {
        return new Entity
        {
            Id = entity.Id,
            Name = entity.Name,
            Stats = entity.Stats.ConvertAll(s => ResourceId.ParseResourceId(s)),
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

    [return: NotNullIfNotNull("userStat")]
    public static UserStats? FromDbModel(UserStatsDbModel? userStat)
    {
        if (userStat is null) return null;
        return new UserStats
        {
            UserId =userStat.UserId,
            DomainId = userStat.DomainId,
            EntityStats = userStat.EntityStats.ConvertAll(FromDbModel),
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
