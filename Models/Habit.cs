using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyTitan.Habits.API.Models;

public class Habit : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public bool IsCompletedToday { get; set; }
    public int StreakCount { get; set; } = 0;

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public User User { get; set; }
}