using ToDoList.Application.Common.Response;
using ToDoList.Application.Tasks.Common;

namespace ToDoList.Application.Tasks.GetAll
{
    public record GetAllTasksQuery(Guid UserId) : IRequest<ErrorOr<ApiResult<List<TaskResponse>>>>;
}