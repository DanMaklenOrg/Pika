using System.Text.RegularExpressions;
using Pika.Model;

namespace Pika.Converter;

public class Deserializer
{
    private static readonly Regex StatTypePattern = new Regex(@"^([A-Z_]+)(\((.*)\))?$");

    private readonly PikaContext _context;

    public Deserializer(PikaContext context)
    {
        _context = context;
    }

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
            SubDomains = node.SubDomains?.ConvertAll(Deserialize) ?? [],
        };
    }

    private Class Deserialize(ClassNode node)
    {
        return new Class
        {
            Id = ParseId(node.Id),
            StatsIds = node.Stats?.ConvertAll(ParseId) ?? [],
            Tags = node.Tags?.ConvertAll(ParseId) ?? [],
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
            Name = node.Title,
            Objectives = node.Objectives.ConvertAll(Deserialize),
        };
    }

    private Objective Deserialize(ObjectiveNode node)
    {
        return new Objective
        {
            Name = node.Title,
            Requirements = node.Requirements.ConvertAll(Deserialize),
        };
    }

    private ObjectiveRequirement Deserialize(ObjectiveRequirementNode node)
    {
        return new ObjectiveRequirement
        {
            Class = ParseId(node.Class),
            Stat = ParseId(node.Stat),
            Min = node.Min,
        };
    }

    private Entity Deserialize(EntityNode node)
    {
        return new Entity
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
            Stats = node.Stats?.ConvertAll(ParseId) ?? [],
            Tags = node.Tags?.ConvertAll(ParseId) ?? [],
            Classes = node.Classes?.ConvertAll(ParseId) ?? [],
            Class = ParseId(node.Classes![0]),
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
        int? min = null;
        int? max = null;
        List<string> enumValues = [];
        switch (type)
        {
            case StatType.IntegerRange:
                min = int.Parse(typeArgs[0]);
                max = int.Parse(typeArgs[1]);
                break;
            case StatType.OrderedEnum:
                enumValues = typeArgs.ToList();
                break;
        }

        return new Stat
        {
            Id = id,
            Name = name,
            Type = type,
            Min = min,
            Max = max,
            EnumValues = enumValues,
        };
    }

    private ResourceId ParseOrInduceId(string? id, string name)
    {
        return string.IsNullOrWhiteSpace(id) ? ResourceId.InduceFromName(name, _context.ScopeDomainId) : ParseId(id);
    }

    private ResourceId ParseId(string id)
    {
        return !id.Contains('/') ? new ResourceId(id, _context.ScopeDomainId) : ResourceId.ParseResourceId(id);
    }
}
