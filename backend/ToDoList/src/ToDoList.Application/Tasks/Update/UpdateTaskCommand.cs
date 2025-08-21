using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Update
{
    public record UpdateTaskCommand(
        Guid Id,
        string? Title,
        string? Description,
        DateTime? DueDate,
        bool? IsCompleted,
        Guid? UserId,
        string? Color
    ) : IRequest<ErrorOr<ApiResult<bool>>>;
}