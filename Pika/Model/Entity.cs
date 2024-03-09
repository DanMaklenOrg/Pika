namespace Pika.Model;

public readonly struct Entity
{
    public ResourceId Id { get; init; }
    public string Name { get; init; }
    public List<ResourceId> Classes { get; init; }
    public List<ResourceId> Stats { get; init; }

    public override string ToString() => Id.FullyQualifiedId;
}
