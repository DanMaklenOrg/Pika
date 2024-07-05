using Pika.Converter;

namespace Pika.Model;

public readonly struct Project
{
    public ResourceId Id { get; init; }
    public string Name { get; init; }
    public List<Objective> Objectives { get; init; }


    public override string ToString() => Id.FullyQualifiedId;
}

public readonly struct Objective
{

}
