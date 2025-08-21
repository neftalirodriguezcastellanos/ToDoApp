using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;
using ToDoList.Domain.Users;

namespace ToDoList.Application.Users.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorOr<ApiResult<UserLoginDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        public async Task<ErrorOr<ApiResult<UserLoginDto>>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.email);
            if (user is null)
            {
                return Errors.User.NotFound;
            }

            if (!await _userRepository.CheckPasswordAsync(request.email, request.password))
            {
                return Errors.User.InvalidCredentials;
            }

            DateTime? expires = DateTime.Now.AddDays(7);
            var token = await _authService.GenerateJwtTokenAsync(user.Email, user.Id, expires);

            var userLoginDto = new UserLoginDto(user.Email, new UserToken(token, expires), user.Roles);

            return new ApiResult<UserLoginDto>(Data: userLoginDto, IsSuccess: true, Message: "Login successful");
        }
    }
}