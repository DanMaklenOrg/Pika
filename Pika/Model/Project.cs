namespace Pika.Model;

public readonly struct Project
{
    public required string Name { get; init; }
    public required  List<Objective> Objectives { get; init; }

    public override string ToString() => Name;
}

public readonly struct Objective
{
    public required string Name { get; init; }

    public required List<ObjectiveRequirement> Requirements { get; init; }
}

public readonly struct ObjectiveRequirement
{
    public required ResourceId Class { get; init; }

    public required ResourceId Stat { get; init; }

    public required int Min { get; init; }
}
