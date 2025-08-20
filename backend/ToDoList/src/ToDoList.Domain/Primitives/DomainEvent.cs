using MediatR;

namespace ToDoList.Domain.Primitives
{
    public record DomainEvent(Guid Id) : INotification;
}