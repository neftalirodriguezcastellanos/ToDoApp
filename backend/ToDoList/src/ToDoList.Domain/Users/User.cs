using ToDoList.Domain.Primitives;

namespace ToDoList.Domain.Users
{
    public sealed class User : AggregateRoot
    {
        public User(Guid id, string fullName, string userName, string email, string passwordHash, List<string>? roles)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PasswordHash = passwordHash;
            Roles = roles;
        }

        private User() { }

        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public List<string>? Roles { get; private set; }
    }
}