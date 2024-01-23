using Pika.Model;

namespace Pika.DomainData.Scrapper;

public interface IScrapper
{
    DomainId DomainId { get; }

    string OutputDirectory { get; }

    Task<Domain> Scrape();
}
