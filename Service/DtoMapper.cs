using Pika.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static GameSummaryDto ToSummaryDto(Game model)
    {
        return new GameSummaryDto { Id = model.Id.ToString(), Name = model.Name };
    }

    public static GameDto ToDto(Game model)
    {
        return new GameDto
        {
            Id = model.Id,
            Name = model.Name,
            Achievements = model.Achievements.ConvertAll(a => new AchievementDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Objectives = a.Objectives.ConvertAll(o => new ObjectiveDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    CriteriaCategory = o.CriteriaCategory,
                }),
                CriteriaCategory = a.CriteriaCategory,
            }),
            Categories = model.Categories.ConvertAll(c => new CategoryDto { Id = c.Id, Name = c.Name }),
            Tags = model.Tags.ConvertAll(t => new TagDto { Id = t.Id, Name = t.Name }),
            Entities = model.Entities.ConvertAll(e => new EntityDto
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
                Tags = e.Tags.ConvertAll(t => t.ToString()),
            })
        };
    }

    public static GameProgressDto ToDto(GameProgress model)
    {
        return new GameProgressDto
        {
            UserId = model.UserId,
            Game = model.Game,
            Completed = model.Completed,
            AchievementProgress = model.AchievementProgress.ConvertAll(a => new AchievementProgressDto
            {
                Achievement = a.Achievement,
                Completed = a.Completed,
                EntitiesDone = a.EntitiesDone.ConvertAll(id => id.ToString()),
                ObjectiveProgress = a.ObjectiveProgress.ConvertAll(o => new ObjectiveProgressDto
                {
                    Objective = o.Objective,
                    Completed = o.Completed,
                    EntitiesDone = o.EntitiesDone.ConvertAll(id => id.ToString()),
                }),
            }),
        };
    }

    public static GameProgress FromDto(GameProgressDto dto)
    {
        return new GameProgress(dto.UserId, dto.Game)
        {
            Completed = dto.Completed,
            AchievementProgress = dto.AchievementProgress.ConvertAll(a => new AchievementProgress(a.Achievement)
            {
                Completed = a.Completed,
                EntitiesDone = a.EntitiesDone.ConvertAll(id => new ResourceId(id)),
                ObjectiveProgress = a.ObjectiveProgress.ConvertAll(o => new ObjectiveProgress(o.Objective)
                {
                    Completed = o.Completed,
                    EntitiesDone = o.EntitiesDone.ConvertAll(id => new ResourceId(id)),
                }),
            }),
        };
    }
}
