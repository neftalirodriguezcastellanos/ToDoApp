using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Tasks.Create;
using ToDoList.Web.Api.Controllers;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Application.Tasks.GetAll;
using ToDoList.Application.Tasks.GetById;
using ToDoList.Application.Tasks.Update;
using ToDoList.Application.Tasks.Delete;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : ApiController
    {
        private readonly ISender _sender;

        public TasksController(ISender sender)
        {
            _sender = sender ?? throw new ArgumentNullException(nameof(sender));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command = command with { UserId = Guid.Parse(userId) };

            var result = await _sender.Send(command);

            return result.Match(
                tasks => Ok(tasks),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _sender.Send(new GetAllTasksQuery(Guid.Parse(userId)));

            return result.Match(
                tasks => Ok(tasks),
                errors => Problem(errors)
            );
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _sender.Send(new GetTaskByIdQuery(taskId, Guid.Parse(userId.ToString())));
            return result.Match(
                task => Ok(task),
                errors => Problem(errors)
            );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            // Asignar el Id del usuario autenticado al comando
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            command = command with { UserId = Guid.Parse(userId) };

            var result = await _sender.Send(command);

            return result.Match(
                tasks => Ok(tasks),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var command = new DeleteTaskCommand(taskId, Guid.Parse(userId));

            var result = await _sender.Send(command);

            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }
    }
}