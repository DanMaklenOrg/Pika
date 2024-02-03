using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("stats")]
    public List<StatDbModel> Stats { get; init; } = new();

    [JsonPropertyName("entries")]
    public List<EntityDbModel> Entities { get; init; } = new();

    [JsonPropertyName("projects")]
    public List<ProjectDbModel> Projects { get; init; } = new();

    [JsonPropertyName("subDomains")]
    public List<DomainDbModel> SubDomains { get; init; } = new();

    protected override void SetKeys()
    {
        PartitionKey = $"Domain#{Id}";
        SortKey = "Domain";
    }
}

public class ProjectDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;
}

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("stats")]
    public List<string> Stats { get; init; } = new();
}

public class StatDbModel
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("type")]
    public string Type { get; init; } = default!;

    [JsonPropertyName("min")]
    public int? Min { get; init; }

    [JsonPropertyName("max")]
    public int? Max { get; init; }

    [JsonPropertyName("enum_values")]
    public List<string>? EnumValues { get; init; }
}
