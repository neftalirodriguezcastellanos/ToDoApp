namespace ToDoList.Application.Common.Response
{
    public record ApiResult<T>(T Data, bool IsSuccess, string Message);
}