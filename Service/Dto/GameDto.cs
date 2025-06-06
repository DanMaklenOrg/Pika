using System.Text.Json.Serialization;

namespace Pika.Service.Dto;

public readonly struct GameDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("achievements")]
    public List<AchievementDto> Achievements { get; init; }

    [JsonPropertyName("classes")]
    public List<ClassDto> Classes { get; init; }

    [JsonPropertyName("entities")]
    public List<EntityDto> Entities { get; init; }
}

public readonly struct AchievementDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("objectives")]
    public List<ObjectiveDto> Objectives { get; init; }
}

public readonly struct ClassDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("attributes")]
    public List<AttributeDto> Attributes { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDto> Stats { get; init; }
}

public readonly struct ObjectiveDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("requirements")]
    public List<RequirementDto> Requirements { get; init; }

    public readonly struct RequirementDto
    {
        [JsonPropertyName("class")]
        public string Class { get; init; }

        [JsonPropertyName("stat")]
        public string Stat { get; init; }

        [JsonPropertyName("min")]
        public int Min { get; init; }
    }
}

public readonly struct EntityDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("attributes")]
    public List<AttributeDto> Attributes { get; init; }

    [JsonPropertyName("stats")]
    public List<StatDto> Stats { get; init; }

    [JsonPropertyName("class")]
    public string Class { get; init; }
}

public readonly struct AttributeDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("value")]
    public int Value { get; init; }
}

public readonly struct StatDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("type")]
    public StatTypeEnumDto Type { get; init; }

    [JsonPropertyName("min")]
    public IntOrAttributeDto? Min { get; init; }

    [JsonPropertyName("max")]
    public IntOrAttributeDto? Max { get; init; }

    [JsonPropertyName("enum_values")]
    public List<string>? EnumValues { get; init; }

    public readonly struct IntOrAttributeDto
    {
        [JsonPropertyName("const_value")]
        public int? ConstValue { get; init; }

        [JsonPropertyName("attribute_id")]
        public string? AttributeId { get; init; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatTypeEnumDto
    {
        Boolean,
        IntegerRange,
        OrderedEnum,
    }
}
