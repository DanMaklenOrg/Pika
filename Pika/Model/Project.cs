namespace Pika.Model;

public readonly record struct Project(string Name)
{
    public required List<Objective> Objectives { get; init; }
}

public readonly record struct Objective(string Name)
{
    public required List<ObjectiveRequirement> Requirements { get; init; }
}

public readonly record struct ObjectiveRequirement(ResourceId Class)
{
    public ResourceId Stat { get; init; } = "_/dummy";

    public int Min { get; init; } = 0;
}
