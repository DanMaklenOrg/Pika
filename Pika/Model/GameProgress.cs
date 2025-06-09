namespace Pika.Model;

public readonly record struct GameProgress(string UserId, ResourceId Game)
{
    public List<AchievementProgress> AchievementProgress { get; init; } = [];
}

public readonly record struct AchievementProgress(ResourceId Achievement)
{
    public List<ObjectiveProgress> ObjectiveProgress { get; init; } = [];
    public List<ResourceId> EntitiesDone { get; init; } = [];
}

public readonly record struct ObjectiveProgress(ResourceId Objective)
{
    public List<ResourceId> EntitiesDone { get; init; } = [];
}
