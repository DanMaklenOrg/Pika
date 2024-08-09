namespace Pika.Model;

public record Domain(ResourceId Id, string Name)
{
    public List<Class> Classes { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Project> Projects { get; init; } = [];
}
