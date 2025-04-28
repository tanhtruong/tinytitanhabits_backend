using TinyTitanHabits.Models;
using TinyTitanHabits.DTOs;
using TinyTitanHabits.Data;
using Microsoft.EntityFrameworkCore;

namespace TinyTitanHabits.Services;

public class TaskService
{
    private readonly AppDbContext _dbContext;

    public TaskService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TaskItem>> GetTasksAsync(Guid userId)
    {
        return await _dbContext.Tasks
            .Where(t => t.UserId == userId && !t.IsDeleted)
            .ToListAsync();
    }

    public async Task<TaskItem> CreateTaskAsync(Guid userId, TaskCreateDto dto)
    {
        var task = new TaskItem
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId
        };

        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();

        return task;
    }

    public async Task<bool> UpdateTaskAsync(Guid taskId, TaskUpdateDto dto)
    {
        var task = await _dbContext.Tasks.FindAsync(taskId);
        if (task == null || task.IsDeleted) return false;

        task.Name = dto.Name;
        task.Description = dto.Description;
        task.IsCompleted = dto.IsCompleted;
        task.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTaskAsync(Guid taskId)
    {
        var task = await _dbContext.Tasks.FindAsync(taskId);
        if (task == null || task.IsDeleted) return false;

        task.IsDeleted = true;
        task.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return true;
    }
}
