using Microsoft.EntityFrameworkCore;

namespace ToDoList.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<ToDoTask> Tasks { get; set; }
    }
}