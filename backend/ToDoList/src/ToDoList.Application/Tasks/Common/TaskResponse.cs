namespace ToDoList.Application.Tasks.Common
{
    public record TaskResponse
    (
        Guid Id,
        string Title,
        string? Description,
        DateTime? DueDate,
        DateTime CreatedAt,
        DateTime? ModifiedAt,
        bool IsCompleted
    );
}