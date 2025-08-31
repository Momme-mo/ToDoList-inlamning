using System.ComponentModel.DataAnnotations;
using Ganss.Xss;

namespace api.ViewModels
{
    public class TaskPostViewModel
    {
        private static readonly HtmlSanitizer _htmlSanitizer = new HtmlSanitizer();

        [Required(ErrorMessage = "Användarnamn Krävs.")]
        [MaxLength(15, ErrorMessage = "Användarnamn får max vara 15 tecken långt.")]
        [MinLength(8, ErrorMessage = "Användarnamn måste vara minst 8 tecken långt.")]

        public required string ItemNumber { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string RoleName { get; set; }

        public void Sanitize()
        {
            ItemNumber = _htmlSanitizer.Sanitize(ItemNumber);
            UserName = _htmlSanitizer.Sanitize(UserName);
            Email = _htmlSanitizer.Sanitize(Email);
            Password = _htmlSanitizer.Sanitize(Password);
            RoleName = _htmlSanitizer.Sanitize(RoleName);
        }
        
        
    }
}