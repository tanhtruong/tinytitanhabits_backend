using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyTitanHabits.Models;

public class Habit : BaseEntity
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool IsCompletedToday { get; set; }
    public int StreakCount { get; set; } = 0;
    public DateTime? LastCompletedDate { get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User? User { get; set; }
}
