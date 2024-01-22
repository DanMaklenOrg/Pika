namespace Pika.Model;

public struct Entity
{
    public ResourceId Id { get; set; }
    public string Name { get; set; }
    public List<ResourceId> Stats { get; set; }

    public override string ToString() => Id.FullyQualifiedId;
}
