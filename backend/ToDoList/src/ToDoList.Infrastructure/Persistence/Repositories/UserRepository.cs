using ToDoList.Domain.Users;
using ToDoList.Infrastructure.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            var roles = await _userManager.GetRolesAsync(user);

            return new User(
                user.Id,
                user.FullName!,
                user.UserName!,
                user.Email!,
                user.PasswordHash!,
                roles.ToList()
                );
        }

        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null)
            {
                return false;
            }

            return await _userManager.CheckPasswordAsync(appUser, password);
        }

        public async Task CreateAsync(User user)
        {
            var newUser = new ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
            };

            var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
            if (user.Roles != null && user.Roles.Any())
            {
                foreach (var role in user.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        await _roleManager.CreateAsync(new ApplicationRole { Name = role });
                    }
                    await _userManager.AddToRoleAsync(newUser, role);
                }
            }
        }

        public async Task<bool> CheckPasswordValidatorAsync(User user)
        {
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var appUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName
            };

            var result = await passwordValidator.ValidateAsync(_userManager, appUser, user.PasswordHash);

            return result.Succeeded;
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = _userManager.Users.ToList().Select(u => new User(
                u.Id,
                u.FullName!,
                u.UserName!,
                u.Email!,
                null,
                _userManager.GetRolesAsync(u).Result.ToList()
            )).ToList();

            return users;
        }

        public async Task<(bool, string[])> UpdateAsync(User user)
        {
            // 1. Buscar el usuario
            var userUpdate = await _userManager.FindByEmailAsync(user.Email);
            if (user == null)
            {
                return (false, new[] { "Usuario no encontrado." });
            }

            // 2. Validar que los roles existan
            foreach (var roleName in user.Roles ?? Enumerable.Empty<string>())
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    return (false, new[] { $"El rol '{roleName}' no existe." });
                }
            }

            // 3. Actualizar datos del usuario
            userUpdate.Email = user.Email;
            userUpdate.UserName = user.Email;
            userUpdate.FullName = user.FullName;

            var updateResult = await _userManager.UpdateAsync(userUpdate);

            if (!updateResult.Succeeded)
            {
                return (false, updateResult.Errors.Select(e => e.Description).ToArray());
            }

            // 4. Actualizar contraseña si se proporciona una nueva
            if (!string.IsNullOrWhiteSpace(user.PasswordHash))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(userUpdate);
                var passwordChangeResult = await _userManager.ResetPasswordAsync(userUpdate, token, user.PasswordHash);

                if (!passwordChangeResult.Succeeded)
                {
                    return (false, passwordChangeResult.Errors.Select(e => e.Description).ToArray());
                }
            }

            // 5. Obtener roles actuales y eliminarlos
            var currentRoles = await _userManager.GetRolesAsync(userUpdate);
            if (currentRoles.Any())
            {
                var removeRolesResult = await _userManager.RemoveFromRolesAsync(userUpdate, currentRoles);

                if (!removeRolesResult.Succeeded)
                {
                    return (false, removeRolesResult.Errors.Select(e => e.Description).ToArray());
                }
            }

            // 6. Agregar nuevos roles
            if ((user.Roles ?? new List<string>()).Count > 0)
            {
                var addRolesResult = await _userManager.AddToRolesAsync(userUpdate, user.Roles);

                if (!addRolesResult.Succeeded)
                {
                    return (false, addRolesResult.Errors.Select(e => e.Description).ToArray());
                }
            }

            return (true, Array.Empty<string>());
        }
    }
}