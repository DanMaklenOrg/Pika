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
            Tags = node.Tags?.ConvertAll(Deserialize) ?? [],
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
            Stats = node.Stats?.ConvertAll<ResourceId>(s => s) ?? [],
            Tags = node.Tags?.ConvertAll<ResourceId>(t => t) ?? [],
        };
    }

    private Tag Deserialize(TagNode node)
    {
        return new Tag
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
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
            Tags = node.Tags?.ConvertAll<ResourceId>(t => t) ?? [],
            Class = node.Class,
        };
    }

    private Stat Deserialize(StatNode node)
    {
        var stat = new Stat
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
        };

        var match = StatTypePattern.Match(node.Type);
        stat.Type = match.Groups[1].Value switch
        {
            "BOOLEAN" => StatType.Boolean,
            "INTEGER_RANGE" => StatType.IntegerRange,
            "ORDERED_ENUM" => StatType.OrderedEnum,
            _ => throw new Exception("Unknown Stat Type"),
        };

        var typeArgs = match.Groups[3].Value.Split(',', StringSplitOptions.TrimEntries);
        switch (stat.Type)
        {
            case StatType.IntegerRange:
                stat.Min = int.Parse(typeArgs[0]);
                stat.Max = int.Parse(typeArgs[1]);
                break;
            case StatType.OrderedEnum:
                stat.EnumValues = typeArgs.ToList();
                break;
        }

        return stat;
    }

    private ResourceId ParseOrInduceId(string? id, string name)
    {
        return string.IsNullOrWhiteSpace(id) ? ResourceId.InduceFromName(name) : id;
    }
}
