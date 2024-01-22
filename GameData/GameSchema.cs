using Pika.DataLayer.Model;

namespace Pika.GameData;

public class GameSchema
{
    public string Version { get; set; } = default!;

    public List<AchievementDbModel> Achievements { get; set; } = new();
}
