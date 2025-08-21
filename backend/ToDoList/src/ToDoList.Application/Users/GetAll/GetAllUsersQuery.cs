using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;

namespace ToDoList.Application.Users.GetAll
{
    public record GetAllUsersQuery() : IRequest<ErrorOr<ApiResult<IReadOnlyList<UserResponse>>>>;
}