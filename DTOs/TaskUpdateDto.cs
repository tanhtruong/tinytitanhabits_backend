using System.ComponentModel.DataAnnotations;

namespace TinyTitanHabits.DTOs;

public class TaskUpdateDto
{
    [Required]
    [MinLength(2)]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }
}
