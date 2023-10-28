using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Pika.DataLayer.Model;

namespace Pika.DataLayer.Repositories;

public class DomainRepo : IDomainRepo
{
    private const string TableName = "PikaTable";
    private const string SkPkIndex = "sk-pk-index";

    private readonly IAmazonDynamoDB _db;

    public DomainRepo(IAmazonDynamoDB db)
    {
        _db = db;
    }

    public async Task Create(DomainDbModel domain)
    {
        var serializedItem = BaseDbModel.SetKeysAndSerialize(domain);
        var request = new PutItemRequest
        {
            TableName = TableName,
            Item = serializedItem,
        };
        await _db.PutItemAsync(request);
    }

    public async Task<DomainDbModel> Get(Guid id)
    {
        var request = new GetItemRequest
        {
            TableName = TableName,
            Key =
            {
                { "pk", new AttributeValue($"Domain#{id.ToString()}") },
                { "sk", new AttributeValue("Domain") },
            }
        };
        var response = await _db.GetItemAsync(request);
        return BaseDbModel.Deserialize<DomainDbModel>(response.Item);
    }

    public async Task<List<DomainDbModel>> GetAll()
    {
        var request = new QueryRequest
        {
            TableName = TableName,
            KeyConditionExpression = "sk = :skValue",
            ExpressionAttributeValues = new Dictionary<string, AttributeValue>
            {
                { ":skValue", new AttributeValue("Domain") },
            },
            IndexName = SkPkIndex,
        };
        var response = await _db.QueryAsync(request);
        return response.Items.ConvertAll(BaseDbModel.Deserialize<DomainDbModel>);
    }
}
