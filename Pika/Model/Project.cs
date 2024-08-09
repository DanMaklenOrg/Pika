namespace Pika.Model;

public readonly struct Project
{
    public required ResourceId Id { get; init; }
    public required string Title { get; init; }
    public required  List<Objective> Objectives { get; init; }


    public override string ToString() => Id;
}

public readonly struct Objective
{
    public required string Title { get; init; }

    public required List<ObjectiveRequirement> Requirements { get; init; }
}

public readonly struct ObjectiveRequirement
{
    public required ResourceId Class { get; init; }

    public required ResourceId Stat { get; init; }

    public required int Min { get; init; }
}
