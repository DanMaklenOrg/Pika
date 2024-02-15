namespace Pika.Model;

public class Entity
{
    public ResourceId Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ResourceId> Classes { get; set; } = new();
    public List<ResourceId> Stats { get; set; } = new();

    public override string ToString() => Id.FullyQualifiedId;
}
