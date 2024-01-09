namespace Pika.GameData.SchemaFetchers;

public interface ISchemaFetcher
{
    Task<GameSchema> FetchSchema(Guid pikaGameId);
}
