using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Delete
{
    public record DeleteTaskCommand(Guid Id, Guid UserId) : IRequest<ErrorOr<ApiResult<bool>>>;
}