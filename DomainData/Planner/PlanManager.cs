namespace Pika.DomainData.Planner;

public class PlanManager
{
    public Plan ConstructPlan(GameSchema pikaSchema, GameSchema sourceSchema)
    {
        var plan = new Plan();
        if (pikaSchema.Version != sourceSchema.Version)
            plan.NewVersion = sourceSchema.Version;

        var pikaAchievements = pikaSchema.Achievements.ToDictionary(a => a.SourceId);
        var sourceAchievements = sourceSchema.Achievements.ToDictionary(a => a.SourceId);

        foreach (var (k, v) in sourceAchievements)
        {
            if (!pikaAchievements.ContainsKey(k)) plan.NewAchievements.Add(v);
            else if (pikaAchievements[k].Name != v.Name) plan.ConflictAchievements.Add((pikaAchievements[k], v));
        }

        plan.DeleteAchievements = pikaSchema.Achievements.Where(x => !sourceAchievements.ContainsKey(x.SourceId)).ToList();

        return plan;
    }
}
