using Pika.Model;

namespace Pika.DomainData;

public interface IScrapper
{
    ResourceId DomainId { get; }

    Task ScrapeInto(Domain domain);
}
