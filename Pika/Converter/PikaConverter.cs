using Pika.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Pika.Converter;

public class PikaConverter
{
    public void Write(Domain domain, TextWriter textWriter)
    {
        var scope = domain.Id;
        new SerializerBuilder()
            .WithTypeConverter(new DomainIdYamlConverter())
            .WithTypeConverter(new ResourceIdYamlConverter(scope))
            .WithTypeConverter(new StatYamlConverter(scope))
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)
            .WithIndentedSequences()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build().Serialize(textWriter, domain);
    }

    public Domain Read(TextReader textReader, string domainId)
    {
        var scope = domainId;
        return new DeserializerBuilder()
            .WithTypeConverter(new DomainIdYamlConverter())
            .WithTypeConverter(new ResourceIdYamlConverter(scope))
            .WithTypeConverter(new StatYamlConverter(scope))
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build().Deserialize<Domain>(textReader);
    }
}
