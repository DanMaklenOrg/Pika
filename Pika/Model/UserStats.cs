namespace Pika.Model;

public struct UserStats
{
    public string UserId { get; set; }

    public DomainId DomainId { get; set; }

    public List<UserEntityStat> EntityStats { get; set; }
}
