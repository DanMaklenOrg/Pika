using Pika.Model;

namespace Pika.DomainData.Scrapper;

public interface IScrapper
{
    DomainId DomainId { get; }

    string OutputDirectory { get; }

    string FileName { get; }

    Task<Domain> Scrape();
}
