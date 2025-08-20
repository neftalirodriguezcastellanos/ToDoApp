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

                var adminRole = "Admin";

                if (!await roleManager.RoleExistsAsync(adminRole))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = adminRole });
                }

                if (!userManager.Users.Any())
                {
                    var adminUser = new ApplicationUser
                    {
                        UserName = "admin@sample.mx",
                        Email = "admin@sample.mx",
                        FullName = "Admin User",
                    };

                    await userManager.CreateAsync(adminUser, "Admin@123");
                    await userManager.AddToRoleAsync(adminUser, adminRole);
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