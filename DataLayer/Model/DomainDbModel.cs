using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDbModel> Stats { get; init; } = [];

    [JsonPropertyName("entries")]
    public List<EntityDbModel> Entities { get; init; } = [];

    [JsonPropertyName("projects")]
    public List<ProjectDbModel> Projects { get; init; } = [];

    [JsonPropertyName("subDomains")]
    public List<DomainDbModel> SubDomains { get; init; } = [];

    protected override void SetKeys()
    {
        PartitionKey = $"Domain#{Id}";
        SortKey = "Domain";
    }
}

public class ProjectDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<string> Stats { get; init; } = [];
}

public class StatDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("min")]
    public int? Min { get; init; }

    [JsonPropertyName("max")]
    public int? Max { get; init; }

    [JsonPropertyName("enum_values")]
    public List<string>? EnumValues { get; init; }
}
