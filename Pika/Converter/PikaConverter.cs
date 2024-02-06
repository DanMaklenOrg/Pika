using Pika.Model;

namespace Pika.Converter;

public class PikaConverter
{
    private readonly PikaFileHandler _fileHandler = new();
    private readonly ContextExtractor _contextExtractor = new();

    public void Write(Domain domain, TextWriter textWriter)
    {
        _fileHandler.Write(new DomainNode(), textWriter);
    }

    public Domain Read(TextReader textReader, string domainId)
    {
        var domainNode = _fileHandler.Read(textReader);
        var context = _contextExtractor.ExtractContext(domainNode);
        var domain = new Deserializer(context).Deserialize(domainNode);
        return domain;
    }
}
