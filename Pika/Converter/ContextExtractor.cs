namespace Pika.Converter;

public class ContextExtractor
{
    public PikaContext ExtractContext(DomainNode node)
    {
        return new PikaContext
        {
            ScopeDomainId = node.Id,
        };
    }
}
