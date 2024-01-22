using Pika.Model;

namespace Pika.GameData.Scrapper;

public interface IScrapper
{
    DomainId DomainId { get; }

    string OutputDirectory { get; }

    Task<Domain> Scrape();
}
