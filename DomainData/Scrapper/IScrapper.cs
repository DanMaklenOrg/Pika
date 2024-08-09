using Pika.Model;

namespace Pika.DomainData.Scrapper;

public interface IScrapper
{
    ResourceId DomainId { get; }

    string OutputDirectory { get; }

    string FileName { get; }

    Task<Domain> Scrape();
}
