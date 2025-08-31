using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace api.ViewModels;
public class RolesPostViewModel
{
    private static readonly HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();

    [Required(ErrorMessage = "Namn krävs.")]
    [MaxLength(13, ErrorMessage = "Namn får inte vara längre än 12 tecken.")]
    [MinLength(8, ErrorMessage = "Namn måste vara minst 8 tecken.")]
    public required string ItemNumber { get; set; }

   
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string RoleName { get; set; }

    public void Sanitize()
    {
        UserName = _htmlSanitizer.Sanitize(UserName);
        Email = _htmlSanitizer.Sanitize(Email);
        Password = _htmlSanitizer.Sanitize(Password);
        RoleName = _htmlSanitizer.Sanitize(RoleName);
    }
}