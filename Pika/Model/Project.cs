namespace Pika.Model;

public struct Project
{
    public ResourceId Id { get; set; }
    public string Name { get; set; }

    public override string ToString() => Id.FullyQualifiedId;
}
