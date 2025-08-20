using Microsoft.EntityFrameworkCore;
using MediatR;
using ToDoList.Domain.Primitives;
using ToDoList.Domain.Tasks;
using ToDoList.Infrastructure.Persistence.Repositories;

namespace ToDoList.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IPublisher _publisher;

        public ITaskRepository Tasks { get; }

        public UnitOfWork(ApplicationDbContext context, IPublisher publisher)
        {
            _context = context;
            _publisher = publisher;
            Tasks = new TaskRepository(context);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 1. Obtener eventos de dominio de entidades agregadas/modificadas
            var domainEvents = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents())
                .ToList();

            // 2. Guardar cambios en la base de datos (EF gestiona la transacción internamente)
            var result = await _context.SaveChangesAsync(cancellationToken);

            // 3. Publicar eventos de dominio después de guardar
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }

            // 4. Limpiar eventos después de publicarlos
            domainEvents.Clear();

            return result;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}