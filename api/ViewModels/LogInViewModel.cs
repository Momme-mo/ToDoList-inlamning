using System.ComponentModel.DataAnnotations;

namespace api.ViewModels;

public class LogInViewModel
{
    [Required, EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}
    