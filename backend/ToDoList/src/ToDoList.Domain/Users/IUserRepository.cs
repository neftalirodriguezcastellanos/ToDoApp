namespace ToDoList.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(string user, string password);
        Task<bool> CheckPasswordValidatorAsync(User user);
        Task CreateAsync(User user);
        Task<List<User>> GetAllAsync();
        Task<(bool, string[])> UpdateAsync(User user);
    }
}