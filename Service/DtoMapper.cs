using Pika.Model;
using Pika.Service.Dto;

namespace Pika.Service;

public static class DtoMapper
{
    public static GameSummaryDto ToSummaryDto(Game model)
    {
        return new GameSummaryDto
        {
            Id = model.Id.ToString(),
            Name = model.Name,
        };
    }

    public static GameDto ToDto(Game model)
    {
        return new GameDto
        {
            Id = model.Id,
            Name = model.Name,
            Entities = model.Entities.ConvertAll(e => new EntityDto
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
            }),
            Achievements = model.Achievements.ConvertAll(a => new AchievementDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Objectives = a.Objectives.ConvertAll(o => new ObjectiveDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    CriteriaCategory = o.CriteriaCategory,
                }),
                CriteriaCategory = a.CriteriaCategory,
            }),
            Categories = model.Categories.ConvertAll(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            }),
        };
    }

    public static GameProgressDto ToDto(GameProgress model)
    {
        return new GameProgressDto
        {
            UserId = model.UserId,
            Game = model.Game,
            AchievementProgress = model.AchievementProgress.ConvertAll(a => new AchievementProgressDto
            {
                Achievement = a.Achievement,
                EntitiesDone = a.EntitiesDone.ConvertAll(id => id.ToString()),
                ObjectiveProgress = a.ObjectiveProgress.ConvertAll(o => new ObjectiveProgressDto
                {
                    Objective = o.Objective,
                    EntitiesDone = o.EntitiesDone.ConvertAll(id => id.ToString()),
                }),
            }),
        };
    }

    public static GameProgress FromDto(GameProgressDto dto)
    {
        return new GameProgress(dto.UserId, dto.Game)
        {
            AchievementProgress = dto.AchievementProgress.ConvertAll(a => new AchievementProgress(a.Achievement)
            {
                EntitiesDone = a.EntitiesDone.ConvertAll(id => new ResourceId(id)),
                ObjectiveProgress = a.ObjectiveProgress.ConvertAll(o => new ObjectiveProgress(o.Objective)
                {
                    EntitiesDone = o.EntitiesDone.ConvertAll(id => new ResourceId(id)),
                }),
            }),
        };
    }
}
