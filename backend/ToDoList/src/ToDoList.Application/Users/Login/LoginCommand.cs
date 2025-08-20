using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;

namespace ToDoList.Application.Users.Login
{
    public record LoginCommand(string email, string password) : IRequest<ErrorOr<ApiResult<UserLoginDto>>>;
}