using Pika.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Pika.Converter;

public class PikaConverter
{
    private readonly SerializerBuilder _serializerBuilder = new SerializerBuilder()
        .WithTypeConverter(new DomainIdYamlConverter())
        .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitNull)
        .WithIndentedSequences()
        .WithNamingConvention(CamelCaseNamingConvention.Instance);

    public void Write(Domain domain, TextWriter textWriter)
    {
        var serializer = _serializerBuilder.WithTypeConverter(new ResourceIdYamlConverter(domain.Id))
            .Build();
        serializer.Serialize(textWriter, domain);
    }
}
