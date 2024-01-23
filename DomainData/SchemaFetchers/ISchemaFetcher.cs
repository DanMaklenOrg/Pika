namespace Pika.DomainData.SchemaFetchers;

public interface ISchemaFetcher
{
    Task<GameSchema> FetchSchema(Guid pikaGameId);
}
