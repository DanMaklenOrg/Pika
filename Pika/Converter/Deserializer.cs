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
            Stats = node.Stats?.ConvertAll(Deserialize) ?? new List<Stat>(),
            Entities = node.Entities?.ConvertAll(Deserialize) ?? new List<Entity>(),
            Projects = node.Projects?.ConvertAll(Deserialize) ?? new List<Project>(),
            SubDomains = node.SubDomains?.ConvertAll(Deserialize) ?? new List<Domain>(),
        };
    }

    private Project Deserialize(ProjectNode node)
    {
        return new Project
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
        };
    }

    private Entity Deserialize(EntityNode node)
    {
        return new Entity
        {
            Id = ParseOrInduceId(node.Id, node.Name),
            Name = node.Name,
            Stats = node.Stats.ConvertAll(ParseId),
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
        return string.IsNullOrWhiteSpace(id) ? ResourceId.InduceFromName(name, _context.ScopeDomainId) : ParseId(id);
    }

    private ResourceId ParseId(string id)
    {
        return !id.Contains('/') ? new ResourceId(id, _context.ScopeDomainId) : ResourceId.ParseResourceId(id);
    }
}
