using Pika.Model;

namespace Pika.GameData;

public interface IScrapper
{
    ResourceId GameId { get; }

    Task ScrapeInto(Game game);
}
