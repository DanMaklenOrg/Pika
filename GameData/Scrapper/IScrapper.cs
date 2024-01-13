using Pika.Model;

namespace Pika.GameData.Scrapper;

public interface IScrapper
{
    string OutputFilePath { get; }

    Task<Domain> Scrape();
}
