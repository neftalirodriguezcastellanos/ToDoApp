using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Users.Update
{
    public record UpdateUserCommand(
        Guid Id,
        string FullName,
        string Email,
        string? PasswordHash,
        List<string> Roles
    ) : IRequest<ErrorOr<ApiResult<bool>>>;
}