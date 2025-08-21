using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;

namespace ToDoList.Application.Users.GetByEmail
{
    public record GetByEmailQuery(string Email) : IRequest<ErrorOr<ApiResult<UserResponse>>>;
}