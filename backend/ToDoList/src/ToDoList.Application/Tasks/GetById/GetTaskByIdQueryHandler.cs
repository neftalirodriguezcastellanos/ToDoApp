using ToDoList.Application.Common.Response;
using ToDoList.Application.Tasks.Common;

namespace ToDoList.Application.Tasks.GetById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, ErrorOr<ApiResult<TaskResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<ApiResult<TaskResponse>>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetById(request.TaskId, Guid.Parse(request.UserId.ToString()));

            if (task is null)
            {
                return Errors.TodoTask.NotFound;
            }

            return new ApiResult<TaskResponse>(
                Data: new TaskResponse
                (
                    task.Id,
                    task.Title,
                    task.Description,
                    task.DueDate,
                    task.CreatedAt,
                    task.ModifiedAt,
                    task.IsCompleted
                ),
                IsSuccess: true,
                Message: "Tarea obtenido correctamente"
            );
        }
    }
}