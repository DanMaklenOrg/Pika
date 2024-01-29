namespace Pika.Model;

public struct UserEntityStat
{
    public ResourceId EntityId { get; set; }

    public ResourceId StatId { get; set; }

    public string Value { get; set; }
}
