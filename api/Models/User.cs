using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class User : IdentityUser
{
    public string UserName { get; set; } = null!;
    public string RoleName { get; set; }

    // Navigation property for ToDoLists
    public List<ToDoList> ToDoLists { get; set; }
}