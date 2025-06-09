using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public class GameProgressDao(IAmazonDynamoDB db) : IGameProgressDao
{
    public async Task Create(GameProgressDbModel gameProgress)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(gameProgress);
        var request = new PutItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Item = serializedItem,
        };
        await db.PutItemAsync(request);
    }

    public async Task<GameProgressDbModel?> Get(string userId, string gameId)
    {
        var request = new GetItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Key =
            {
                { "pk", new AttributeValue($"UserStat#{userId}#{gameId}") },
                { "sk", new AttributeValue("UserStat") },
            }
        };
        var response = await db.GetItemAsync(request);

        if (!response.IsItemSet) return null;
        return BaseDbModel.Deserialize<GameProgressDbModel>(response.Item);
    }
}
