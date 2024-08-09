namespace Pika.Model;

public readonly record struct UserEntityStat(ResourceId EntityId, ResourceId StatId, string Value);

public readonly record struct UserStats(string UserId, DomainId DomainId)
{
    public List<UserEntityStat> EntityStats { get; init; } = [];

    public List<ResourceId> CompletedProjectIds { get; init; } = [];
}
