namespace Pika.Service.Dto;

public readonly struct ObjectiveDto
{
    public ObjectiveTypeDto ObjectiveTypeDto { get; init; }

    public string Description { get; init; }
}
