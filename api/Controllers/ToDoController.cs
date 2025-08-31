using api.Data;
using api.Models;
using api.ViewModels;
using Ganss.Xss;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;
    private readonly HtmlSanitizer _htmlSanitizer = new();

    [HttpPost("add")]
    public async Task<ActionResult> AddTask(TaskPostViewModel model)
    {
        if (!ModelState.IsValid) return ValidationProblem();

        model.UserName = _htmlSanitizer.Sanitize(model.UserName);
        model.Email = _htmlSanitizer.Sanitize(model.Email);
        model.Password = _htmlSanitizer.Sanitize(model.Password);
        model.RoleName = _htmlSanitizer.Sanitize(model.RoleName);

        ModelState.Clear();
        TryValidateModel(model);

        if (!ModelState.IsValid) return ValidationProblem();


        var toDoList = new ToDoList
        {
            Title = $"{model.UserName}"
        };

        _context.ToDoLists.Add(toDoList);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(FindToDoList), new { id = toDoList.Id });
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult> ListAllToDo()
    {
        var ToDos = await _context.ToDoLists.ToListAsync();
        return Ok(new { success = true, data = ToDos });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindToDoList(string id)
    {
        var toDoList = await _context.ToDoLists.FindAsync(id);

        if (toDoList is null) return NotFound();

        return Ok(new { success = true, data = toDoList });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveTask(string id)
    {
        var task = await _context.ToDoLists.FindAsync(id);

        if (task is null) return NotFound();

        _context.ToDoLists.Remove(task);

        await _context.SaveChangesAsync();

        return NoContent();
    }

}
