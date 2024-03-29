using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace Pika.DataLayer;

public abstract class BaseDbModel
{
    [JsonPropertyName("pk")]
    public string PartitionKey { get; set; } = string.Empty;

    [JsonPropertyName("sk")]
    public string SortKey { get; set; } = string.Empty;

    protected abstract void SetKeys();

    public static Dictionary<string, AttributeValue> SetKeysAndSerialize<T>(T item)
        where T : BaseDbModel
    {
        item.SetKeys();
        var json = JsonSerializer.Serialize(item, JsonOptions);
        var doc = Document.FromJson(json);
        return doc.ToAttributeMap();
    }

    public static T Deserialize<T>(Dictionary<string, AttributeValue> serializedItem)
        where T : BaseDbModel
    {
        var doc = Document.FromAttributeMap(serializedItem);
        var json = doc.ToJson();
        return JsonSerializer.Deserialize<T>(json, JsonOptions)!;
    }

    private static readonly JsonSerializerOptions JsonOptions = new() { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
}
