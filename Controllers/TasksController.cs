using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyTitanHabits.DTOs;
using TinyTitanHabits.Services;
using System.Security.Claims;

namespace TinyTitanHabits.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TaskController : ControllerBase
{
    private readonly TaskService _taskService;

    public TaskController(TaskService taskService)
    {
        _taskService = taskService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }

    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await _taskService.GetTasksAsync(GetUserId());
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask(TaskCreateDto dto)
    {
        var task = await _taskService.CreateTaskAsync(GetUserId(), dto);
        return Ok(task);
    }

    [HttpPut("{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, TaskUpdateDto dto)
    {
        var success = await _taskService.UpdateTaskAsync(taskId, dto);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        var success = await _taskService.DeleteTaskAsync(taskId);
        if (!success) return NotFound();

        return NoContent();
    }
}
