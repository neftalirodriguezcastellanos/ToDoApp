using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Users.Create
{
    public record CreateUserCommand(string Email, string FullName, string Password, List<string>? Roles) : IRequest<ErrorOr<ApiResult<bool>>>;
}