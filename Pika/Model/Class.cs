namespace Pika.Model;

public class Class
{
    public ResourceId Id { get; set; }
    public List<ResourceId> Stats { get; set; } = new();
}
