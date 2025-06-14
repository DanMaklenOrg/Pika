using System.Diagnostics.CodeAnalysis;
using Pika.DataLayer.Model;
using Pika.Model;

namespace Pika.DataLayer.Mapper;

public static class DbModelMapper
{
    public static GameDbModel ToDbModel(Game game)
    {
        return new GameDbModel
        {
            Id = game.Id,
            Name = game.Name,
            SteamAppId = game.SteamAppId,
            Achievements = game.Achievements.ConvertAll(a => new AchievementDbModel
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Objectives = a.Objectives.ConvertAll(o => new ObjectiveDbModel
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    CriteriaCategory = o.CriteriaCategory,
                }),
                CriteriaCategory = a.CriteriaCategory,
            }),
            Categories = game.Categories.ConvertAll(c => new CategoryDbModel { Id = c.Id, Name = c.Name }),
            Tags = game.Tags.ConvertAll(t => new TagDbModel { Id = t.Id, Name = t.Name }),
            Entities = game.Entities.ConvertAll(e => new EntityDbModel
            {
                Id = e.Id,
                Name = e.Name,
                Category = e.Category,
                Tags = e.Tags.ConvertAll(t => t.ToString()),
            })
        };
    }

    [return: NotNullIfNotNull("model")]
    public static Game? FromDbModel(GameDbModel? model)
    {
        if (model is null) return null;
        return new Game(model.Id, model.Name)
        {
            SteamAppId = model.SteamAppId,
            Achievements = model.Achievements?.ConvertAll(a => new Achievement(a.Id, a.Name)
            {
                Description = a.Description,
                Objectives = a.Objectives?.ConvertAll(o => new Objective(o.Id, o.Name)
                {
                    Description = o.Description,
                    CriteriaCategory = FromDbModel(o.CriteriaCategory),
                }) ?? [],
                CriteriaCategory = FromDbModel(a.CriteriaCategory),
            }) ?? [],
            Categories = model.Categories?.ConvertAll(c => new Category(c.Id, c.Name)) ?? [],
            Tags = model.Tags?.ConvertAll(t => new Tag(t.Id, t.Name)) ?? [],
            Entities = model.Entities?.ConvertAll(e => new Entity(e.Id, e.Name, e.Category)
            {
                Tags = e.Tags?.ConvertAll(t => FromDbModel(t).Value) ?? [],
            }) ?? [],
        };
    }

    [return: NotNullIfNotNull("model")]
    private static ResourceId? FromDbModel(string? model)
    {
        return model is null ? (ResourceId?)null : model;
    }

    public static GameProgressDbModel ToDbModel(GameProgress model)
    {
        return new GameProgressDbModel
        {
            UserId = model.UserId,
            Game = model.Game,
            Completed = model.Completed,
            AchievementProgress = model.AchievementProgress.ConvertAll(a => new AchievementProgressDbModel
            {
                Achievement = a.Achievement,
                Completed = a.Completed,
                EntitiesDone = a.EntitiesDone.ConvertAll(e => e.ToString()),
                ObjectiveProgress = a.ObjectiveProgress.ConvertAll(o => new ObjectiveProgressDbModel
                {
                    Objective = o.Objective,
                    Completed = o.Completed,
                    EntitiesDone = o.EntitiesDone.ConvertAll(e => e.ToString()),
                }),
            }),
        };
    }

    [return: NotNullIfNotNull("model")]
    public static GameProgress? FromDbModel(GameProgressDbModel? model)
    {
        if (model is null) return null;
        return new GameProgress(model.UserId, model.Game)
        {
            Completed = model.Completed,
            AchievementProgress = model.AchievementProgress?.ConvertAll(a => new AchievementProgress(a.Achievement)
            {
                Completed = a.Completed,
                EntitiesDone = a.EntitiesDone?.ConvertAll(e => new ResourceId(e)) ?? [],
                ObjectiveProgress = a.ObjectiveProgress?.ConvertAll(o => new ObjectiveProgress(o.Objective)
                {
                    Completed = o.Completed,
                    EntitiesDone = o.EntitiesDone?.ConvertAll(e => new ResourceId(e)) ?? [],
                }) ?? [],
            }) ?? [],
        };
    }
}
