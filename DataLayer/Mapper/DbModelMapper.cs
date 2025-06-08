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
            Objectives = achievement.Objectives.ConvertAll(ToDbModel),
        };
    }

    private static ObjectiveDbModel ToDbModel(Objective objective)
    {
        return new ObjectiveDbModel
        {
            Id = objective.Id,
            Name = objective.Name,
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

    public static UserStatsDbModel ToDbModel(UserStats userStats)
    {
        return new UserStatsDbModel
        {
            UserId = userStats.UserId,
            GameId = userStats.GameId,
            EntityStats = userStats.EntityStats.ConvertAll(ToDbModel),
        };
    }

    private static UserEntityStatDbModel ToDbModel(UserEntityStat userEntityStat)
    {
        return new UserEntityStatDbModel
        {
            EntityId = userEntityStat.EntityId,
            StatId = userEntityStat.StatId,
            Value = userEntityStat.Value,
        };
    }

    [return: NotNullIfNotNull("userStats")]
    public static UserStats? FromDbModel(UserStatsDbModel? userStats)
    {
        if (userStats is null) return null;
        return new UserStats
        {
            UserId = userStats.UserId,
            GameId = userStats.GameId,
            EntityStats = userStats.EntityStats.ConvertAll(FromDbModel),
        };
    }

    private static UserEntityStat FromDbModel(UserEntityStatDbModel userEntityStatDbModel)
    {
        return new UserEntityStat
        {
            EntityId = userEntityStatDbModel.EntityId,
            StatId = userEntityStatDbModel.StatId,
            Value = userEntityStatDbModel.Value,
        };
    }
}
