using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Dao;

public class DomainDao : IDomainDao
{
    private readonly IAmazonDynamoDB _db;

    public DomainDao(IAmazonDynamoDB db)
    {
        _db = db;
    }

    public async Task Create(DomainDbModel domain)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(domain);
        var request = new PutItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Item = serializedItem,
        };
        await _db.PutItemAsync(request);
    }

    public async Task<DomainDbModel?> Get(string id)
    {
        var request = new GetItemRequest
        {
            TableName = DynamoDbConstants.TableName,
            Key =
            {
                { "pk", new AttributeValue($"Domain#{id}") },
                { "sk", new AttributeValue("Domain") },
            }
        };
        var response = await _db.GetItemAsync(request);

        if (!response.IsItemSet) return null;
        return BaseDbModel.Deserialize<DomainDbModel>(response.Item);
    }

    public async Task<List<DomainDbModel>> GetAll()
    {
        var request = new QueryRequest
        {
            TableName = DynamoDbConstants.TableName,
            KeyConditionExpression = "sk = :skValue",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":skValue", new AttributeValue("Domain") },
            },
            IndexName = DynamoDbConstants.SkPkIndex,
        };
        var response = await _db.QueryAsync(request);
        return response.Items.ConvertAll(BaseDbModel.Deserialize<DomainDbModel>);
    }
}
