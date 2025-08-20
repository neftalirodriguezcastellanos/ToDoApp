using ToDoList.Application.Common.Response;
using ToDoList.Application.Users.Common;
using ToDoList.Domain.Users;

namespace ToDoList.Application.Users.GetAll
{
    public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<ApiResult<IReadOnlyList<UserResponse>>>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ErrorOr<ApiResult<IReadOnlyList<UserResponse>>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepository.GetAllAsync();

            var result = users.Select(user => new UserResponse(
                user.Id,
                user.FullName,
                user.Email,
                user.UserName,
                user.Roles.Select(role => role).ToList()
            )).ToList();

            return new ApiResult<IReadOnlyList<UserResponse>>(
                Data: result,
                IsSuccess: true,
                Message: "User list retrieved successfully"
            );
        }
    }
}