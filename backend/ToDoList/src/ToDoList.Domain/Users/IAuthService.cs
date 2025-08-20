namespace ToDoList.Domain.Users
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(string email, DateTime? expires = null);
    }
}