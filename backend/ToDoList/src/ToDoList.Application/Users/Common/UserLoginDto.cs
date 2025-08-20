namespace ToDoList.Application.Users.Common
{
    public record UserLoginDto(string Email, UserToken DataAuth, List<string> Roles);

    public record UserToken(string Token, DateTime? Expires);
}