using Pika.Model;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Pika.Converter;

public class ResourceIdYamlConverter : IYamlTypeConverter
{
    private readonly DomainId _scope;

    public ResourceIdYamlConverter(DomainId scope)
    {
        _scope = scope;
    }

    public bool Accepts(Type type) => type == typeof(ResourceId);

    public object? ReadYaml(IParser parser, Type type)
    {
        throw new NotImplementedException();
    }

    public void WriteYaml(IEmitter emitter, object? value, Type type)
    {
        var id = (ResourceId)value!;
        var idString = id.FullyQualifiedId;
        if (id.Domain.FullyQualifiedId == _scope.FullyQualifiedId) idString = id.Id;
        emitter.Emit(new Scalar(idString));
    }
}
