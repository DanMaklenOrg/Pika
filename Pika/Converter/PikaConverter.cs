using Pika.Model;

namespace Pika.Converter;

public class PikaConverter
{
    private readonly PikaFileHandler _fileHandler = new();

    public void Write(Domain domain, TextWriter textWriter)
    {
        var domainNode = new Serializer().Serialize(domain);
        _fileHandler.Write(domainNode, textWriter);
    }

    public Domain Read(TextReader textReader)
    {
        var domainNode = _fileHandler.Read(textReader);
        var domain = new Deserializer().Deserialize(domainNode);
        return domain;
    }
}
