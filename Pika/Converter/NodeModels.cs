using YamlDotNet.Serialization;

namespace Pika.Converter;

public readonly struct DomainNode
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public List<StatNode>? Stats { get; init; }
    public List<ClassNode>? Classes { get; init; }
    public List<EntityNode>? Entities { get; init; }
    public List<ProjectNode>? Projects { get; init; }
}

public readonly struct EntityNode
{
    public string? Id { get; init; }
    public string Name { get; init; }
    public string Class { get; init; }
    public List<string>? Stats { get; init; }
}

public readonly struct StatNode
{
    public string? Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}

public readonly struct ProjectNode
{
    public string Name { get; init; }
    public List<ObjectiveNode> Objectives { get; init; }
}

public readonly struct ObjectiveNode
{
    public string Name { get; init; }

    [YamlMember(Alias = "require")]
    public List<ObjectiveRequirementNode> Requirements { get; init; }
}

public readonly struct ObjectiveRequirementNode
{
    public string Class { get; init; }

    public string Stat { get; init; }

    public int Min { get; init; }
}

public readonly struct ClassNode
{
    public string Id { get; init; }
    public List<string>? Stats { get; init; }
}
