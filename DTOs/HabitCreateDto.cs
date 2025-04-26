using System.ComponentModel.DataAnnotations;

namespace TinyTitanHabits.DTOs;

public class HabitCreateDto
{
    [Required]
    [MinLength(2)]
    public required string Name { get; set; }

    public string? Description { get; set; }
}
