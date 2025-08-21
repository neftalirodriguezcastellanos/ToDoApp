
using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Create
{
    public record CreateTaskCommand(
        string Title,
        string? Description,
        DateTime? DueDate,
        Guid? UserId,
        string? Color
    ) : IRequest<ErrorOr<ApiResult<bool>>>;
}