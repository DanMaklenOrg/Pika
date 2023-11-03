using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public class AchievementRepo : IAchievementRepo
{
    private readonly IAmazonDynamoDB _db;

    public AchievementRepo(IAmazonDynamoDB db)
    {
        _db = db;
    }

    public async Task Create(AchievementDbModel achievement)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(achievement);
        var request = new PutItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Item = serializedItem,
        };
        await _db.PutItemAsync(request);
    }

    public async Task<List<AchievementDbModel>> GetAll(Guid gameId)
    {
        var request = new QueryRequest
        {
            TableName = DynamoDbConstants.TableName,
            KeyConditionExpression = "pk = :pkVal and begins_with(sk, :skPrefix)",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":pkVal", new AttributeValue($"Game#{gameId}") },
                { ":skPrefix", new AttributeValue("Achievement#") },
            },
        };
        var response = await _db.QueryAsync(request);
        return response.Items.ConvertAll(BaseDbModel.Deserialize<AchievementDbModel>);
    }
}
