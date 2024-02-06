using Pika.Model;

namespace Pika.Converter;

public class PikaConverter
{
    private readonly PikaFileHandler _fileHandler = new();
    private readonly ContextExtractor _contextExtractor = new();

    public void Write(Domain domain, TextWriter textWriter)
    {
        var context = _contextExtractor.ExtractContext(domain);
        var domainNode = new Serializer(context).Serialize(domain);
        _fileHandler.Write(domainNode, textWriter);
    }

    public Domain Read(TextReader textReader)
    {
        var domainNode = _fileHandler.Read(textReader);
        var context = _contextExtractor.ExtractContext(domainNode);
        var domain = new Deserializer(context).Deserialize(domainNode);
        return domain;
    }
}
