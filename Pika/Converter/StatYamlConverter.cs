using System.Text.RegularExpressions;
using Pika.Model;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Pika.Converter;

internal class StatYamlConverter : IYamlTypeConverter
{
    private static readonly Regex StatTypePattern = new Regex(@"^([A-Z_]+)(\((.*)\))?$");

    private readonly DomainId _scope;

    public StatYamlConverter(DomainId scope)
    {
        _scope = scope;
    }

    public bool Accepts(Type type) => type == typeof(Stat);

    public object? ReadYaml(IParser parser, Type type)
    {
        Dictionary<string, string> properties = new();
        parser.Consume<MappingStart>();
        while (!parser.TryConsume<MappingEnd>(out _))
        {
            var key = parser.Consume<Scalar>().Value;
            var val = parser.Consume<Scalar>().Value;
            properties[key] = val;
        }

        var stat = new Stat
        {
            Id = ResourceId.ParseResourceId(properties["id"], _scope),
            Name = properties["name"],
        };

        var match = StatTypePattern.Match(properties["type"]);
        stat.Type = match.Groups[1].Value switch
        {
            "BOOLEAN" => StatType.Boolean,
            "INTEGER_RANGE" => StatType.IntegerRange,
            _ => throw new Exception("Unknown Stat Type"),
        };

        var typeArgs = match.Groups[3].Value.Split(',');
        switch (stat.Type)
        {
            case StatType.IntegerRange:
                stat.Min = int.Parse(typeArgs[0]);
                stat.Max = int.Parse(typeArgs[1]);
                break;
        }

        return stat;
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        throw new NotImplementedException();
    }
}
