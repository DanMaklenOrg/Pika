using System.Text.Json.Serialization;

namespace Pika.DataLayer.Model;

public class GameDbModel : BaseDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDbModel>? Entities { get; init; }

    [JsonPropertyName("achievements")]
    public List<AchievementDbModel>? Achievements { get; init; }

    [JsonPropertyName("classes")]
    public List<ClassDbModel>? Classes { get; init; }

    protected override void SetKeys()
    {
        PartitionKey = $"Game#{Id}";
        SortKey = "Game";
    }
}

public class AchievementDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("objectives")]
    public required List<ObjectiveDbModel> Objectives { get; init; }
}

public class ObjectiveDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }


    [JsonPropertyName("requirements")]
    public required List<RequirementDbModel> Requirements { get; init; }

    public class RequirementDbModel
    {
        [JsonPropertyName("class")]
        public required string Class { get; init; }

        [JsonPropertyName("stat")]
        public required string Stat { get; init; }

        [JsonPropertyName("min")]
        public required int Min { get; init; }
    }
}

public class EntityDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("class")]
    public required string Class { get; init; }

    [JsonPropertyName("attributes")]
    public required List<AttributeDbModel> Attributes { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDbModel>? Stats { get; init; }
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
    public IntOrAttributeDbModel? Min { get; init; }

    [JsonPropertyName("max")]
    public IntOrAttributeDbModel? Max { get; init; }

    [JsonPropertyName("enum_values")]
    public List<string>? EnumValues { get; init; }

    public class IntOrAttributeDbModel
    {
        [JsonPropertyName("const_value")]
        public int? ConstValue { get; init; }

        [JsonPropertyName("attribute_id")]
        public string? AttributeId { get; init; }
    }
}

public class ClassDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("attributes")]
    public required List<AttributeDbModel> Attributes { get; init; }

    [JsonPropertyName("stats")]
    public required List<StatDbModel> Stats { get; init; }
}

public class AttributeDbModel
{
    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("val")]
    public required int Value { get; init; }
}
