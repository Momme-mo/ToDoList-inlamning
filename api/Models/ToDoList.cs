namespace api.Models;

public class ToDoList
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string UserId { get; set; }

    // Navigation properties
    public User User { get; set; }
}

