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
            Entities = game.Entities.ConvertAll(ToDbModel),
            Achievements = game.Achievements.ConvertAll(ToDbModel),
            Categories = game.Categories.ConvertAll(model => new CategoryDbModel(model.Id, model.Name)),
        };
    }

    private static AchievementDbModel ToDbModel(Achievement achievement)
    {
        return new AchievementDbModel
        {
            Id = achievement.Id,
            Name = achievement.Name,
            Description = achievement.Description,
            Objectives = achievement.Objectives.ConvertAll(ToDbModel),
            CriteriaCategory = achievement.CriteriaCategory,
        };
    }

    private static ObjectiveDbModel ToDbModel(Objective objective)
    {
        return new ObjectiveDbModel
        {
            Id = objective.Id,
            Name = objective.Name,
            Description = objective.Description,
            CriteriaCategory = objective.CriteriaCategory,
        };
    }

    private static EntityDbModel ToDbModel(Entity entity)
    {
        return new EntityDbModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Category = entity.Category,
        };
    }

    [return: NotNullIfNotNull("model")]
    public static Game? FromDbModel(GameDbModel? model)
    {
        if (model is null) return null;
        return new Game(model.Id, model.Name)
        {
            Achievements = model.Achievements?.ConvertAll(FromDbModel) ?? [],
            Categories = model.Categories?.ConvertAll(FromDbModel) ?? [],
            Entities = model.Entities?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static Achievement FromDbModel(AchievementDbModel model)
    {
        return new Achievement(model.Id, model.Name)
        {
            Description = model.Description,
            Objectives = model.Objectives?.ConvertAll(FromDbModel) ?? [],
            CriteriaCategory = model.CriteriaCategory is null ? (ResourceId?)null : model.CriteriaCategory,
        };
    }

    private static Objective FromDbModel(ObjectiveDbModel model)
    {
        return new Objective(model.Id, model.Name)
        {
            Description = model.Description,
            CriteriaCategory = model.CriteriaCategory is null ? (ResourceId?)null : model.CriteriaCategory,
        };
    }

    private static Category FromDbModel(CategoryDbModel model)
    {
        return new Category(model.Id, model.Name);
    }

    private static Entity FromDbModel(EntityDbModel model)
    {
        return new Entity(model.Id, model.Name, model.Category);
    }

    public static GameProgressDbModel ToDbModel(GameProgress model)
    {
        return new GameProgressDbModel
        {
            UserId = model.UserId,
            Game = model.Game,
            AchievementProgress = model.AchievementProgress.ConvertAll(ToDbModel),
        };
    }
    private static AchievementProgressDbModel ToDbModel(AchievementProgress a)
    {
        return new AchievementProgressDbModel
        {
            Achievement = a.Achievement,
            EntitiesDone = a.EntitiesDone.ConvertAll(e => e.ToString()),
            ObjectiveProgress = a.ObjectiveProgress.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveProgressDbModel ToDbModel(ObjectiveProgress model)
    {
        return new ObjectiveProgressDbModel
        {
            Objective = model.Objective,
            EntitiesDone = model.EntitiesDone.ConvertAll(e => e.ToString()),
        };
    }

    [return: NotNullIfNotNull("model")]
    public static GameProgress? FromDbModel(GameProgressDbModel? model)
    {
        if (model is null) return null;
        return new GameProgress
        {
            UserId = model.UserId,
            Game = model.Game,
            AchievementProgress = model.AchievementProgress?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static AchievementProgress FromDbModel(AchievementProgressDbModel a)
    {
        return new AchievementProgress(a.Achievement)
        {
            EntitiesDone = a.EntitiesDone?.ConvertAll(e => new ResourceId(e)) ?? [],
            ObjectiveProgress = a.ObjectiveProgress?.ConvertAll(FromDbModel) ?? [],
        };
    }

    private static ObjectiveProgress FromDbModel(ObjectiveProgressDbModel model)
    {
        return new ObjectiveProgress(model.Objective)
        {
            EntitiesDone = model.EntitiesDone?.ConvertAll(e => new ResourceId(e)) ?? [],
        };
    }
}
