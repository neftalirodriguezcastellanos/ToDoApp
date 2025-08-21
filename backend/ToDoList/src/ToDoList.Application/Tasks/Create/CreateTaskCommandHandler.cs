using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Create
{
    public sealed class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, ErrorOr<ApiResult<bool>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<ApiResult<bool>>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = new ToDoTask
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserId = request.UserId ?? throw new ArgumentNullException(nameof(request.UserId)),
                Color = request.Color,
            };

            await _unitOfWork.Tasks.Add(newTask);
            var result = await _unitOfWork.SaveChangesAsync();


            return new ApiResult<bool>(
                Data: result > 0,
                IsSuccess: result > 0,
                Message: "La tarea se creó correctamente."
            );
        }

    }
}