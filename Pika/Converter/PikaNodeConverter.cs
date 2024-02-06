using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Pika.Converter;

public class PikaFileHandler
{
    public void Write(DomainNode domain, TextWriter textWriter)
    {
        new SerializerBuilder()
            .ConfigureDefaultValuesHandling(DefaultValuesHandling.OmitDefaults | DefaultValuesHandling.OmitNull | DefaultValuesHandling.OmitEmptyCollections)
            .WithIndentedSequences()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build().Serialize(textWriter, domain);
    }

    public DomainNode Read(TextReader textReader)
    {
        return new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build().Deserialize<DomainNode>(textReader);
    }
}
