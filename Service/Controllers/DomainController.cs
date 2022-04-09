using Microsoft.AspNetCore.Mvc;
using Pika.Service.Dto;

namespace Pika.Service.Controllers;

[ApiController]
[Route("api/domain")]
public class DomainController : ControllerBase
{
    [HttpGet]
    public List<DomainDto> GetDomainList()
    {
        return new List<DomainDto>
        {
            new DomainDto
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = "Warframe",
            },
        };
    }

    [HttpGet("{domainId}/profile")]
    public DomainProfileDto GetDomainProfile(string domainId)
    {
        return new DomainProfileDto
        {
            ProjectList = new List<EntryDto>
            {
                new EntryDto
                {
                    Title = "Max Mastery Rank",
                    Children = new List<EntryDto>
                    {
                        new EntryDto
                        {
                            Title = "Warframes",
                            Objectives = new List<ObjectiveDto>
                            {
                                new ObjectiveDto
                                {
                                    Description = "Obtain",
                                    ObjectiveTypeDto = ObjectiveTypeDto.Check,
                                },
                            },
                        },
                    },
                },
            },
        };
    }
}
