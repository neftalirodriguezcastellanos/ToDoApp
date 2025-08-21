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

        public async Task<ToDoTask?> GetById(Guid id, Guid userId)
        {
            // Obtiene la tarea por Id y filtra por el UserId
            return await _context.Tasks
                .Where(t => t.Id == id && t.UserId == userId)
                .FirstOrDefaultAsync();
        }

        public async Task Add(ToDoTask task)
        {
            await _context.Tasks.AddAsync(task);
        }

        public async Task<List<ToDoTask>> GetAll(Guid userId)
        {
            // Obtiene todas las tareas del usuario especificado
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
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