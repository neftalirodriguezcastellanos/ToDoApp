using ToDoList.Application.Users.Login;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Application.Users.Create;
using ToDoList.Application.Users.GetAll;
using ToDoList.Application.Users.GetByEmail;
using ToDoList.Application.Users.Update;

namespace ToDoList.Web.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ApiController
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());

            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var result = await _mediator.Send(new GetByEmailQuery(email));

            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Match(
                success => Ok(success),
                errors => Problem(errors)
            );
        }
    }
}