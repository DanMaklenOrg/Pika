namespace Pika.DataLayer.Model;

public class DomainDbModel : BaseDbModel
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; init; } = default!;

    public override void SetKeys()
    {
        PartitionKey = $"Domain#{Id.ToString()}";
        SortKey = "Domain";
    }
}
