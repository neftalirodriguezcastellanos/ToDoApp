namespace ToDoList.Application.Users.Common
{
    public record UserResponse(Guid Id,
                                string FullName,
                                string Email,
                                string UserName,
                                List<string> Roles);
}