using Microsoft.AspNetCore.Identity;
using api.Models;

namespace api.Data;

public static class Seed
{
    public static async Task SeedData(DataContext context, UserManager<User> userManager)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<User>
            {
                new() { UserName = "Mohammed", RoleName = "Admin" },
                new() { UserName = "Michael", RoleName = "User" }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Password1!");
            }
        }

        if (context.ToDoLists.Any()) return;

        var toDoLists = new List<ToDoList>
        {
            new() { Title = "Tanka bilen", UserId = userManager.Users.First().Id },
            new() { Title = "Tvätta bilen", UserId = userManager.Users.First().Id },
            new() { Title = "Byt turbo", UserId = userManager.Users.Skip(1).First().Id },
            new() { Title = "Fyll spolarvätska", UserId = userManager.Users.Skip(2).First().Id },
        };

        context.ToDoLists.AddRange(toDoLists);
        await context.SaveChangesAsync();


    }
}