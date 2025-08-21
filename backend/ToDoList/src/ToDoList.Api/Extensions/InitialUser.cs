using System.Threading.Tasks;
using ToDoList.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ToDoList.Web.Api.Extensions
{
    public static class InitialUser
    {
        public static async Task CreateInitialUser(this WebApplication app)
        {
            try
            {
                using var scope = app.Services.CreateScope();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var role = "User";

                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }

                if (!userManager.Users.Any())
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = "user@todolist.mx",
                        Email = "user@todolist.mx",
                        FullName = "Initial User",
                    };

                    await userManager.CreateAsync(adminUser, "User@123");
                    await userManager.AddToRoleAsync(adminUser, role);
                }
            }
            catch (Exception ex)
            {
                // Log the exception if necessary
                Console.WriteLine($"Error creating initial user: {ex.Message}");
            }
        }
    }
}