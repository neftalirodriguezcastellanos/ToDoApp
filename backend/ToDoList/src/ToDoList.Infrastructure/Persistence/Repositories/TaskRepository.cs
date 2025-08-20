using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Tasks;

namespace ToDoList.Infrastructure.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoTask?> GetById(Guid id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task Add(ToDoTask task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task<List<ToDoTask>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public void Update(ToDoTask task)
        {
            _context.Tasks.Update(task);
        }

        public void Delete(ToDoTask task)
        {
            _context.Tasks.Remove(task);
        }
    }
}