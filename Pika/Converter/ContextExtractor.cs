using Pika.Model;

namespace Pika.Converter;

public class ContextExtractor
{
    public PikaContext ExtractContext(DomainNode domain)
    {
        return new PikaContext
        {
            ScopeDomainId = domain.Id,
        };
    }

    public PikaContext ExtractContext(Domain domain)
    {
        return new PikaContext
        {
            ScopeDomainId = domain.Id,
        };
    }
}
