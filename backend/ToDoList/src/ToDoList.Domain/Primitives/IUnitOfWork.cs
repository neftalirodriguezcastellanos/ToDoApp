using ToDoList.Domain.Tasks;

namespace ToDoList.Domain.Primitives
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskRepository Tasks { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}