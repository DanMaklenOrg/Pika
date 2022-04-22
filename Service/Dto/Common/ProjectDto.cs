using System.Text.Json.Serialization;

namespace Pika.Service.Dto.Common;

public struct ProjectDto
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; }

    [JsonPropertyName("objectives")]
    public List<ObjectiveDto> Objectives { get; set; }

    [JsonConstructor]
    public ProjectDto(string title, List<ObjectiveDto> objectives, string? id = null)
    {
        this.Id = id ?? Guid.Empty.ToString("N");
        this.Title = title;
        this.Objectives = objectives;
    }
}
