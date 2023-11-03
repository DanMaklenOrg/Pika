namespace Pika.GameDataScrapper.SchemaFetchers;

public interface ISchemaFetcher
{
    Task<GameSchema> FetchSchema(Guid pikaGameId);
}
