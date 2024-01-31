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

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        var val = (Stat)value!;

        var id = val.Id.FullyQualifiedId;
        if (val.Id.Domain.FullyQualifiedId == _scope.FullyQualifiedId) id = val.Id.Id;

        emitter.Emit(new MappingStart());

        emitter.Emit(new Scalar("id"));
        emitter.Emit(new Scalar(id));
        emitter.Emit(new Scalar("name"));
        emitter.Emit(new Scalar(val.Name));

        emitter.Emit(new Scalar("type"));
        switch (val.Type)
        {
            case StatType.Boolean:
                emitter.Emit(new Scalar("BOOLEAN"));
                break;
            case StatType.IntegerRange:
                emitter.Emit(new Scalar($"INTEGER_RANGE({val.Min}, {val.Max})"));
                break;
            case StatType.OrderedEnum:
                emitter.Emit(new Scalar($"ORDERED_ENUM({string.Join(", ", val.EnumValues!)})"));
                break;
            default:
                throw new Exception("Unknown Stat Type");
        }

        emitter.Emit(new MappingEnd());
    }
}
