namespace Pika.Model;

public readonly record struct Domain(ResourceId Id, string Name)
{
    public List<Stat> Stats { get; init; } = [];
    public List<Class> Classes { get; init; } = [];
    public List<Entity> Entities { get; init; } = [];
    public List<Project> Projects { get; init; } = [];
}
