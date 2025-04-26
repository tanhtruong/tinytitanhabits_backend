using System.ComponentModel.DataAnnotations;

namespace TinyTitanHabits.Models;

public class User : BaseEntity
{
    [Required]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string PasswordHash { get; set; }

    public ICollection<Habit> Habits { get; set; } = new List<Habit>();
}
