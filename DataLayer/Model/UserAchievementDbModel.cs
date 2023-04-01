using System.ComponentModel.DataAnnotations.Schema;

namespace Pika.DataLayer.Model;

[Table("UserAchievement")]
public class UserAchievementDbModel
{
    public Guid UserId { get; set; }

    public Guid AchievementId { get; set; }

    [ForeignKey(nameof(AchievementId))]
    public AchievementDbModel Achievement { get; set; } = default!;

    public bool UnlockState { get; set; }
}
