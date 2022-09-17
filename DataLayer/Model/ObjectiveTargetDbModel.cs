namespace Pika.DataLayer.Model;

public class ObjectiveTargetDbModel
{
    public ObjectiveDbModel Objective { get; set; } = default!;

    public EntryDbModel Target { get; set; } = default!;
}
