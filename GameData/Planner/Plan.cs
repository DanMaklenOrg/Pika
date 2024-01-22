using Pika.DataLayer.Model;

namespace Pika.GameData.Planner;

public struct Plan
{
    public Plan()
    {
    }

    public string? NewVersion { get; set; } = null;

    public List<AchievementDbModel> NewAchievements { get; set; } = new();

    public List<AchievementDbModel> DeleteAchievements { get; set; } = new();

    public List<(AchievementDbModel pika, AchievementDbModel src)> ConflictAchievements { get; set; } = new();
}
