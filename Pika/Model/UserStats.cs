namespace Pika.Model;

public readonly record struct UserStats(string UserId, ResourceId GameId)
{
    public List<UserEntityStat> EntityStats { get; init; } = [];
}

public readonly record struct UserEntityStat(ResourceId EntityId, ResourceId StatId, string Value);
