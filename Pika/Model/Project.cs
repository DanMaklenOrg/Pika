namespace Pika.Model;

public readonly struct Project
{
    public ResourceId Id { get; init; }
    public string Name { get; init; }

    public override string ToString() => Id.FullyQualifiedId;
}
