namespace TinyTitanHabits.DTOs;

public class HabitReadDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsCompletedToday { get; set; }
    public int StreakCount { get; set; }
    public DateTime CreatedAt { get; set; }
}
