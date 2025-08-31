using Ganss.Xss;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.ViewModels;

namespace api001.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(SignInManager<User> signInManager) : ControllerBase
    {
        private readonly HtmlSanitizer _htmlSanitizer = new();

        [HttpGet("ListAllUsers")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult<IEnumerable<User>>> ListAllUsers()
        {
            var users = await signInManager.UserManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("register")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.Email = _htmlSanitizer.Sanitize(model.Email);
            model.UserName = _htmlSanitizer.Sanitize(model.UserName);
            model.Password = _htmlSanitizer.Sanitize(model.Password);

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid) return ValidationProblem();

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return Ok();

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        [HttpPost("logout")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return NoContent();
        }


        [HttpPost("login")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult> Login(LogInViewModel model)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.Email = _htmlSanitizer.Sanitize(model.Email);
            model.Password = _htmlSanitizer.Sanitize(model.Password);

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid) return ValidationProblem();

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);

            if (result.Succeeded) return Ok();

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return ValidationProblem();
        }

        [HttpPost("registerUser")]
        [EnableRateLimiting("fixed")]
        public async Task<ActionResult> RegisterUser(PostUserViewModel model)
        {
            if (!ModelState.IsValid) return ValidationProblem();

            model.UserName = _htmlSanitizer.Sanitize(model.UserName);

            ModelState.Clear();
            TryValidateModel(model);

            if (!ModelState.IsValid) return ValidationProblem();

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await signInManager.UserManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return Ok();

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }
    }
}