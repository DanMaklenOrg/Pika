using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDbModel>? Stats { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDbModel>? Entities { get; init; }

    [JsonPropertyName("projects")]
    public List<ProjectDbModel>? Projects { get; init; }

    [JsonPropertyName("classes")]
    public List<ClassDbModel>? Classes { get; init; }

    protected override void SetKeys()
    {
        PartitionKey = $"Domain#{Id}";
        SortKey = "Domain";
    }
}

public class ProjectDbModel
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("objectives")]
    public required List<ObjectiveDbModel> Objectives { get; init; }
}

public class ObjectiveDbModel
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("requirements")]
    public required List<ObjectiveRequirementDbModel> Requirements { get; init; }
}

public class ObjectiveRequirementDbModel
{
    [JsonPropertyName("class")]
    public required string Class { get; init; }

    [JsonPropertyName("stat")]
    public required string Stat { get; init; }

    [JsonPropertyName("min")]
    public required int Min { get; init; }
}

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("class")]
    public required string Class { get; init; }

    [JsonPropertyName("stats")]
    public List<string>? Stats { get; init; }
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

public class ClassDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("stats")]
    public required List<string>? Stats { get; init; }
}
