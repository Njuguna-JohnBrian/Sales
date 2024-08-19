using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RegistrationDto
{
    [Required(ErrorMessage = "Email Address is required"), EmailAddress(ErrorMessage = "Email Address is invalid")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "First name is required"), MinLength(3, ErrorMessage = "First name is too short")]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required"), MinLength(3, ErrorMessage = "Last name is too short")]
    public required string LastName { get; set; }

    [Required(ErrorMessage = "Password is required"), MinLength(6, ErrorMessage = "Password is too short")]
    public required string Password { get; set; }

    [Required(ErrorMessage = "Please confirm your password"),
     Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }

    public Guid? UserRoleId { get; set; } = Guid.Empty;
}
