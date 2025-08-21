using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Application.Common.Response;
using ToDoList.Application.Tasks.Common;

namespace ToDoList.Application.Tasks.GetAll
{
    public sealed class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, ErrorOr<ApiResult<List<TaskResponse>>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTasksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<ApiResult<List<TaskResponse>>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _unitOfWork.Tasks.GetAll(request.UserId);

            var taskResponses = tasks.Select(task => new TaskResponse(
                    task.Id,
                    task.Title,
                    task.Description,
                    task.DueDate,
                    task.CreatedAt,
                    task.ModifiedAt,
                    task.IsCompleted,
                    task.Color
                )).ToList();

            return new ApiResult<List<TaskResponse>>(
                Data: taskResponses ?? new List<TaskResponse>(),
                IsSuccess: true,
                Message: "Tareas obtenidas correctamente");
        }
    }
}