using Pika.Model;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Pika.Converter;

public class DomainIdYamlConverter : IYamlTypeConverter
{
    public bool Accepts(Type type) => type == typeof(DomainId);

    public object? ReadYaml(IParser parser, Type type)
    {
        var idNode = parser.Consume<Scalar>();
        return DomainId.ParseDomainId(idNode.Value);
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        var id = (DomainId)value!;
        emitter.Emit(new Scalar(id.FullyQualifiedId));
    }
}
