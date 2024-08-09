namespace Pika.Model;

public struct UserStats
{
    public string UserId { get; init; }

    public ResourceId DomainId { get; init; }

    public List<UserEntityStat> EntityStats { get; init; } = [];

    public UserStats(string userId, string domainId)
    {
        UserId = userId;
        DomainId = domainId;
    }
}
