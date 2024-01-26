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
        };
    }
}
