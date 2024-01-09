namespace Pika;

public readonly struct Entity
{
    public PikaId Id { get; init; }

    public string Name { get; init; }

    public List<PikaId> Stats { get; init; }

    public override string ToString() => Id.FullyQualifiedId;
}
