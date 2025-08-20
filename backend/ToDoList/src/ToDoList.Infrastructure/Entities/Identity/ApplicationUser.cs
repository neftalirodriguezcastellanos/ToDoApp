using Microsoft.AspNetCore.Identity;

namespace ToDoList.Infrastructure.Entities.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FullName { get; set; }
    }
}