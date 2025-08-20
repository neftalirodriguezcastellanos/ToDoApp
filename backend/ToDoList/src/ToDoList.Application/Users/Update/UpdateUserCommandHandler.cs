using ToDoList.Application.Common.Response;
using ToDoList.Domain.Users;

namespace ToDoList.Application.Users.Update
{
    public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<ApiResult<bool>>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<ApiResult<bool>>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Errors.User.NotFound;
            }

            var updateUser = new User(
                request.Id,
                request.FullName,
                request.Email,
                request.Email,
                request.PasswordHash,
                request.Roles
            );

            if (!await _userRepository.CheckPasswordValidatorAsync(updateUser))
            {
                return Errors.User.InvalidPassword;
            }

            var (success, errors) = await _userRepository.UpdateAsync(updateUser);
            if (!success)
            {
                return Error.Failure
                (
                    "UserUpdateFailed", string.Join(", ", errors)
                );
            }

            return new ApiResult<bool>
            (
                Data: true,
                IsSuccess: true,
                Message: "User updated successfully"
            );
        }
    }
}