using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;
using ToDoList.Domain.Users;

namespace ToDoList.Application.Users.GetByEmail
{
    public sealed class GetByEmailQueryHandler : IRequestHandler<GetByEmailQuery, ErrorOr<ApiResult<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public GetByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<ApiResult<UserResponse>>> Handle(GetByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return Errors.User.NotFound;
            }

            var userResponse = new UserResponse(
                user.Id,
                user.FullName,
                user.Email,
                user.UserName,
                user.Roles.Select(role => role).ToList()
            );

            return new ApiResult<UserResponse>(
                Data: userResponse,
                IsSuccess: true,
                Message: "User retrieved successfully"
            );
        }
    }
}