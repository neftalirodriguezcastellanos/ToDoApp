using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Update
{
    public sealed class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, ErrorOr<ApiResult<bool>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }


        public async Task<ErrorOr<ApiResult<bool>>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetById(request.Id, Guid.Parse(request.UserId.ToString()));
            if (task is null)
            {
                return Errors.TodoTask.NotFound;
            }

            bool hasChanges = false;

            // Comparar y actualizar solo si hay cambios
            if (!string.IsNullOrWhiteSpace(request.Title) && task.Title != request.Title)
            {
                task.Title = request.Title;
                hasChanges = true;
            }

            if (request.Description != null && task.Description != request.Description)
            {
                task.Description = request.Description;
                hasChanges = true;
            }

            if (request.DueDate.HasValue)
            {
                if (request.DueDate < DateTime.Today)
                    return Errors.TodoTask.InvalidDueDate;

                if (task.DueDate != request.DueDate)
                {
                    task.DueDate = request.DueDate;
                    hasChanges = true;
                }
            }

            if (request.IsCompleted.HasValue && task.IsCompleted != request.IsCompleted.Value)
            {
                task.IsCompleted = request.IsCompleted.Value;
                hasChanges = true;
            }

            if (request.Color != null && task.Color != request.Color)
            {
                task.Color = request.Color;
                hasChanges = true;
            }

            // Si no hay cambios, no guardar
            if (!hasChanges)
            {
                return new ApiResult<bool>(
                    Data: true,
                    IsSuccess: true,
                    Message: "No hubo cambios para actualizar."
                );
            }

            // Marcar como modificado si usas audit fields
            task.ModifiedAt = DateTime.Now;

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult<bool>(
                Data: result > 0,
                IsSuccess: result > 0,
                Message: "La tarea se actualizó correctamente."
            );
        }
    }
}