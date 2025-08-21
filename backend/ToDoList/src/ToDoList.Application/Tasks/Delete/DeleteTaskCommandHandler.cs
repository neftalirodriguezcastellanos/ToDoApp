using ToDoList.Application.Common.Response;

namespace ToDoList.Application.Tasks.Delete
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, ErrorOr<ApiResult<bool>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<ErrorOr<ApiResult<bool>>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _unitOfWork.Tasks.GetById(request.Id, request.UserId);
            if (task is null)
            {
                return Errors.TodoTask.NotFound;
            }

            task.IsDeleted = true;
            task.DeletedAt = DateTime.Now;

            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new ApiResult<bool>(
                Data: result > 0,
                IsSuccess: result > 0,
                Message: "La tarea se eliminó correctamente."
            );
        }
    }
}