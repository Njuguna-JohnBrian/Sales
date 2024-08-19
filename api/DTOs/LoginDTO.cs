using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class LoginDto
{
    [Required(ErrorMessage = "Email Address is required"), EmailAddress(ErrorMessage = "Email Address is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; set; }
}
