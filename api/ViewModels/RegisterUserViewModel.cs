using System.ComponentModel.DataAnnotations;

namespace api.ViewModels;


public class RegisterUserViewModel
{
    [Required]
    public required string UserName { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string RoleName { get; set; }
    
    
}