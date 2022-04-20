using Pika.Service.Dto.Common;

namespace Pika.Service.Dto.Response;

public struct GetDomainProfileResponseDto
{
    public EntryDto RootEntry { get; set; }

    public List<ProjectDto> Projects { get; set; }
}
