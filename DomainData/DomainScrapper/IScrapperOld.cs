using Pika.Model;

namespace Pika.DomainData.DomainScrapper;

public interface IScrapperOld
{
    ResourceId DomainId { get; }

    Task<Domain> Scrape();
}
