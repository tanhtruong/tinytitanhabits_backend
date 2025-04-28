using TinyTitanHabits.Data;
using TinyTitanHabits.Models;
using TinyTitanHabits.DTOs;
using Microsoft.EntityFrameworkCore;

namespace TinyTitanHabits.Services;

public class HabitService
{
    private readonly AppDbContext _dbContext;

    public HabitService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<HabitReadDto>> GetHabitsAsync(Guid userId)
    {
        var habits = await _dbContext.Habits
            .Where(h => h.UserId == userId && !h.IsDeleted)
            .ToListAsync();

        return habits.Select(h => new HabitReadDto
        {
            Id = h.Id,
            Name = h.Name,
            Description = h.Description,
            CreatedAt = h.CreatedAt
        }).ToList();
    }

    public async Task<HabitReadDto> CreateHabitAsync(Guid userId, HabitCreateDto dto)
    {
        var habit = new Habit
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };

        _dbContext.Habits.Add(habit);
        await _dbContext.SaveChangesAsync();

        return new HabitReadDto
        {
            Id = habit.Id,
            Name = habit.Name,
            Description = habit.Description,
            CreatedAt = habit.CreatedAt
        };
    }

    public async Task<bool> UpdateHabitAsync(Guid habitId, HabitUpdateDto dto)
    {
        var habit = await _dbContext.Habits.FindAsync(habitId);
        if (habit == null || habit.IsDeleted) return false;

        habit.Name = dto.Name;
        habit.Description = dto.Description;
        habit.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteHabitAsync(Guid habitId)
    {
        var habit = await _dbContext.Habits.FindAsync(habitId);
        if (habit == null || habit.IsDeleted) return false;

        habit.IsDeleted = true;
        habit.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CompleteHabitTodayAsync(Guid habitId)
    {
        var habit = await _dbContext.Habits
            .Include(h => h.Completions)
            .FirstOrDefaultAsync(h => h.Id == habitId && !h.IsDeleted);

        if (habit == null) return false;

        var today = DateTime.UtcNow.Date;

        // Check if already completed today
        bool alreadyCompleted = habit.Completions.Any(c => c.Date == today);
        if (alreadyCompleted) return false;

        var completion = new HabitCompletion
        {
            HabitId = habit.Id,
            Date = today
        };

        _dbContext.HabitCompletions.Add(completion);
        await _dbContext.SaveChangesAsync();

        return true;
    }

}
