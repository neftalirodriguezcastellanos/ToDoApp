using ToDoList.Application.Common.Response;
using ToDoList.Domain.Users;

namespace ToDoList.Application.Users.Create
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<ApiResult<bool>>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<ApiResult<bool>>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var existingUser = await _userRepository.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return Errors.User.EmailAlreadyExists;
            }

            var user = new User(
                Guid.NewGuid(),
                request.FullName,
                request.Email,
                request.Email,
                request.Password,
                request.Roles ?? new List<string>()
            );

            if (!await _userRepository.CheckPasswordValidatorAsync(user))
            {
                return Errors.User.InvalidPassword;
            }

            await _userRepository.CreateAsync(user);

            return new ApiResult<bool>(
                Data: true,
                IsSuccess: true,
                Message: "User created successfully"
            );
        }
    }
}