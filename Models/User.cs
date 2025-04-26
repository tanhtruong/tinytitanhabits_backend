using System.ComponentModel.DataAnnotations;

namespace TinyTitan.Habits.API.Models;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;

    public ICollection<Habit> Habits { get; set; }
}