namespace Pika.Model;

public struct UserStats
{
    public string UserId { get; init; }

    public DomainId DomainId { get; init; }

    public List<UserEntityStat> EntityStats { get; init; } = [];

    public List<ResourceId> CompletedProjectIds { get; init; } = [];

    public UserStats(string userId, string domainId)
    {
        UserId = userId;
        DomainId = domainId;
    }
}
