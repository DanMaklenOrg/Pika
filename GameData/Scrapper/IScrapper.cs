namespace Pika.GameData.Scrapper;

public interface IScrapper
{
    Task<Domain> Scrape();
}
