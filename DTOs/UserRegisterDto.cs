using System.ComponentModel.DataAnnotations;

namespace TinyTitanHabits.DTOs;

public class UserRegisterDto
{
    [Required]
    [MinLength(2)]
    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    public required string Password { get; set; }
}
