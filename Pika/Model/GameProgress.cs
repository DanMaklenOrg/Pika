namespace Pika.Model;

public readonly record struct GameProgress(string UserId, ResourceId Game)
{
    public bool Completed { get; init; }
    public List<AchievementProgress> AchievementProgress { get; init; } = [];
}

public readonly record struct AchievementProgress(ResourceId Achievement)
{
    public bool Completed { get; init; }
    public List<ObjectiveProgress> ObjectiveProgress { get; init; } = [];
    public List<ResourceId> EntitiesDone { get; init; } = [];
}

public readonly record struct ObjectiveProgress(ResourceId Objective)
{
    public bool Completed { get; init; }
    public List<ResourceId> EntitiesDone { get; init; } = [];
}
