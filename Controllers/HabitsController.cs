using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TinyTitanHabits.DTOs;
using TinyTitanHabits.Services;

namespace TinyTitanHabits.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HabitsController : ControllerBase
{
    private readonly HabitService _habitService;

    public HabitsController(HabitService habitService)
    {
        _habitService = habitService;
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User ID not found in token."));
    }

    [HttpGet]
    public async Task<IActionResult> GetHabits()
    {
        var habits = await _habitService.GetHabitsAsync(GetUserId());
        return Ok(habits);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHabit(HabitCreateDto dto)
    {
        var habit = await _habitService.CreateHabitAsync(GetUserId(), dto);
        return Ok(habit);
    }

    [HttpPut("{habitId}")]
    public async Task<IActionResult> UpdateHabit(Guid habitId, HabitUpdateDto dto)
    {
        var success = await _habitService.UpdateHabitAsync(habitId, dto);
        if (!success) return NotFound();

        return NoContent();
    }

    [HttpDelete("{habitId}")]
    public async Task<IActionResult> DeleteHabit(Guid habitId)
    {
        var success = await _habitService.DeleteHabitAsync(habitId);
        if (!success) return NotFound();

        return NoContent();
    }


    [HttpPost("{habitId}/complete")]
    public async Task<IActionResult> CompleteHabit(Guid habitId)
    {
        var success = await _habitService.CompleteHabitTodayAsync(habitId);
        if (!success) return BadRequest(new { message = "Already completed today or habit not found." });

        return Ok();
    }

}
