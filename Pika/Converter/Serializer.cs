using Pika.Model;

namespace Pika.Converter;

public class Serializer
{
    private readonly PikaContext _context;

    public Serializer(PikaContext context)
    {
        _context = context;
    }

    public DomainNode Serialize(Domain domain)
    {
        return new DomainNode
        {
            Id = domain.Id.FullyQualifiedId,
            Name = domain.Name,
            Stats = domain.Stats.ConvertAll(Serialize),
            Tags = domain.Tags.ConvertAll(Serialize),
            Classes = domain.Classes.ConvertAll(Serialize),
            Entities = domain.Entities.ConvertAll(Serialize),
            Projects = domain.Projects.ConvertAll(Serialize),
            SubDomains = domain.SubDomains.ConvertAll(Serialize),
        };
    }

    private ClassNode Serialize(Class node)
    {
        return new ClassNode
        {
            Id = MinifyId(node.Id),
            Stats = node.Stats.ConvertAll(MinifyId),
            Tags = node.Tags.ConvertAll(MinifyId),
        };
    }

    private TagNode Serialize(Tag node)
    {
        return new TagNode
        {
            Id = MinifyId(node.Id),
            Name = node.Name,
        };
    }

    private ProjectNode Serialize(Project node)
    {
        return new ProjectNode
        {
            Id = MinifyId(node.Id),
            Title = node.Title,
            Objectives = node.Objectives.ConvertAll(Serialize),
        };
    }

    private ObjectiveNode Serialize(Objective node)
    {
        return new ObjectiveNode
        {
            Title = node.Title,
            Requirements = node.Requirements.ConvertAll(Serialize),
        };
    }

    private ObjectiveRequirementNode Serialize(ObjectiveRequirement node)
    {
        return new ObjectiveRequirementNode
        {
            Class = MinifyId(node.Class),
            Stat = MinifyId(node.Stat),
            Min = node.Min,
        };
    }

    private EntityNode Serialize(Entity node)
    {
        return new EntityNode
        {
            Id = MinifyId(node.Id),
            Name = node.Name,
            Stats = node.Stats.ConvertAll(MinifyId),
            Tags = node.Tags.ConvertAll(MinifyId),
            Classes = node.Classes.ConvertAll(MinifyId),
        };
    }

    private StatNode Serialize(Stat stat)
    {
        return new StatNode
        {
            Id = MinifyId(stat.Id),
            Name = stat.Name,
            Type = stat.Type switch
            {
                StatType.Boolean => "BOOLEAN",
                StatType.IntegerRange => $"INTEGER_RANGE({stat.Min}, {stat.Max})",
                StatType.OrderedEnum => $"ORDERED_ENUM({string.Join(", ", stat.EnumValues!)})",
                _ => throw new Exception("Unknown Stat Type"),
            },
        };
    }

    private string MinifyId(ResourceId id)
    {
        return id.Domain.FullyQualifiedId == _context.ScopeDomainId.FullyQualifiedId ? id.Id : id.FullyQualifiedId;
    }
}
