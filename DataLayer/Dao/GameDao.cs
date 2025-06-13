using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public class GameDao(IAmazonDynamoDB db) : IGameDao
{
    public async Task Create(GameDbModel game)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(game);
        var request = new PutItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Item = serializedItem,
        };
        await db.PutItemAsync(request);
    }

    public async Task<GameDbModel?> Get(string id)
    {
        var request = new GetItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Key =
            {
                { "pk", new AttributeValue($"Game#{id}") },
                { "sk", new AttributeValue("Game") },
            }
        };
        var response = await db.GetItemAsync(request);

        if (!response.IsItemSet) return null;
        return BaseDbModel.Deserialize<GameDbModel>(response.Item);
    }

    public async Task<List<GameDbModel>> GetAll()
    {
        var request = new QueryRequest
        {
            TableName = DynamoDbConstants.TableName,
            KeyConditionExpression = "sk = :skValue",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":skValue", new AttributeValue("Game") },
            },
            IndexName = DynamoDbConstants.SkPkIndex,
            FilterExpression = "attribute_exists(categories)",
        };
        var response = await db.QueryAsync(request);
        return response.Items.ConvertAll(BaseDbModel.Deserialize<GameDbModel>);
    }
}
