using System.Text.RegularExpressions;
using Pika.Model;

namespace Pika.Converter;

public class Deserializer
{
    private static readonly Regex StatTypePattern = new Regex(@"^([A-Z_]+)(\((.*)\))?$");

    public Domain Deserialize(DomainNode node)
    {
        return new Domain
        {
            Id = node.Id,
            Name = node.Name ?? string.Empty,
            Stats = node.Stats?.ConvertAll(Deserialize) ?? [],
            Classes = node.Classes?.ConvertAll(Deserialize) ?? [],
            Entities = node.Entities?.ConvertAll(Deserialize) ?? [],
            Projects = node.Projects?.ConvertAll(Deserialize) ?? [],
        };
    }

    private Class Deserialize(ClassNode node)
    {
        return new Class
        {
            Id = node.Id,
            // Stats = node.Stats.ConvertAll(Deserialize) ?? [],
        };
    }

    private Project Deserialize(ProjectNode node)
    {
        return new Project
        {
            Name = node.Name,
            Objectives = node.Objectives.ConvertAll(Deserialize),
        };
    }

    private Objective Deserialize(ObjectiveNode node)
    {
        return new Objective
        {
            Name = node.Name,
            Requirements = node.Requirements.ConvertAll(Deserialize),
        };
    }

    private ObjectiveRequirement Deserialize(ObjectiveRequirementNode node)
    {
        return new ObjectiveRequirement
        {
            Class = node.Class,
            Stat = node.Stat,
            Min = node.Min,
        };
    }

    private Entity Deserialize(EntityNode node)
    {
        return new Entity
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
            Stats = node.Stats?.ConvertAll<ResourceId>(s => s) ?? [],
            Class = node.Class,
        };
    }

    private Stat Deserialize(StatNode node)
    {
        var id = ParseOrInduceId(node.Id, node.Name);
        var name = node.Name;

        var match = StatTypePattern.Match(node.Type);
        var type = match.Groups[1].Value switch
        {
            "BOOLEAN" => StatType.Boolean,
            "INTEGER_RANGE" => StatType.IntegerRange,
            "ORDERED_ENUM" => StatType.OrderedEnum,
            _ => throw new Exception("Unknown Stat Type"),
        };
        var typeArgs = match.Groups[3].Value.Split(',', StringSplitOptions.TrimEntries);
        return type switch
        {
            StatType.IntegerRange => new Stat(id, name, type)
            {
                Min = int.Parse(typeArgs[0]),
                Max = int.Parse(typeArgs[1]),
            },
            StatType.OrderedEnum => new Stat(id, name, type)
            {
                EnumValues = typeArgs.ToList(),
            },
            _ => new Stat(id, name, type),
        };
    }

    private ResourceId ParseOrInduceId(string? id, string name)
    {
        return string.IsNullOrWhiteSpace(id) ? ResourceId.InduceFromName(name) : id;
    }
}
