namespace ToDoList.Domain.Users
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(string email, Guid userId, DateTime? expires = null);
    }
}