using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyTitanHabits.Models;

public class HabitCompletion : BaseEntity
{
    [Required]
    public DateTime Date { get; set; } = DateTime.UtcNow.Date;

    [ForeignKey("Habit")]
    public Guid HabitId { get; set; }

    public Habit? Habit { get; set; }
}
