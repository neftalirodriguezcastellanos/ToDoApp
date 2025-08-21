using ToDoList.Application.Common.Response;
using ToDoList.Application.Tasks.Common;

namespace ToDoList.Application.Tasks.GetById
{
    public record GetTaskByIdQuery(Guid TaskId, Guid UserId) : IRequest<ErrorOr<ApiResult<TaskResponse>>>;
}