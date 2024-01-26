using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public class UserStatsDao : IUserStatsDao
{
    private readonly IAmazonDynamoDB _db;

    public UserStatsDao(IAmazonDynamoDB db)
    {
        _db = db;
    }

    public async Task Create(UserStatsDbModel domain)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(domain);
        var request = new PutItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Item = serializedItem,
        };
        await _db.PutItemAsync(request);
    }

    public async Task<UserStatsDbModel?> Get(string userId, string domainId)
    {
        var request = new GetItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Key =
            {
                { "pk", new AttributeValue($"UserStat#{userId}#{domainId}") },
                { "sk", new AttributeValue("UserStat") },
            }
        };
        var response = await _db.GetItemAsync(request);

        if (!response.IsItemSet) return null;
        return BaseDbModel.Deserialize<UserStatsDbModel>(response.Item);
    }
}
