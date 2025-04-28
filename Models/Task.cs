using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TinyTitanHabits.Models;

public class TaskItem : BaseEntity
{
    [Required]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    [ForeignKey("User")]
    public Guid UserId { get; set; }

    public User? User { get; set; }
}
