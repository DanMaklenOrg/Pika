using Pika.DataLayer.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static DomainDto ToDto(DomainDbModel model)
    {
        return new DomainDto
        {
            Id = model.Id,
            Name = model.Name,
        };
    }
}
