namespace Pika.Model;

public struct UserStats
{
    public string UserId { get; set; }

    public DomainId DomainId { get; set; }

    public List<UserEntityStat> EntityStats { get; set; } = new();

    public List<ResourceId> CompletedProjectIds { get; set; } = new();

    public UserStats(string userId, DomainId domainId)
    {
        UserId = userId;
        DomainId = domainId;
    }
}
