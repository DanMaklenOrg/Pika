using Pika.Model;

namespace Pika.Converter;

public class Serializer
{
    public DomainNode Serialize(Domain domain)
    {
        return new DomainNode
        {
            Id = domain.Id,
            Name = domain.Name,
            Stats = domain.Stats.ConvertAll(Serialize),
            Classes = domain.Classes.ConvertAll(Serialize),
            Entities = domain.Entities.ConvertAll(Serialize),
            Projects = domain.Projects.ConvertAll(Serialize),
        };
    }

    private ClassNode Serialize(Class node)
    {
        return new ClassNode
        {
            Id = node.Id,
            // Stats = node.Stats.ConvertAll<string>(s => s),
        };
    }

    private ProjectNode Serialize(Project node)
    {
        return new ProjectNode
        {
            Name = node.Name,
            Objectives = node.Objectives.ConvertAll(Serialize),
        };
    }

    private ObjectiveNode Serialize(Objective node)
    {
        return new ObjectiveNode
        {
            Name = node.Name,
            Requirements = node.Requirements.ConvertAll(Serialize),
        };
    }

    private ObjectiveRequirementNode Serialize(ObjectiveRequirement node)
    {
        return new ObjectiveRequirementNode
        {
            Class = node.Class,
            Stat = node.Stat,
            Min = node.Min,
        };
    }

    private EntityNode Serialize(Entity node)
    {
        return new EntityNode
        {
            Id = node.Id,
            Name = node.Name,
            Stats = node.Stats.ConvertAll<string>(s => s),
            Class = node.Class,
        };
    }

    private StatNode Serialize(Stat stat)
    {
        return new StatNode
        {
            Id = stat.Id,
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
}
