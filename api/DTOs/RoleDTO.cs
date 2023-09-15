using System.ComponentModel.DataAnnotations;

namespace api.DTOs;

public class RoleDto
{
    [Required(ErrorMessage = "Role name is required")]
    public required string RoleName { get; set; }

    [Required(ErrorMessage = "Role description is required")]
    public required string RoleDescription { get; set; }
}