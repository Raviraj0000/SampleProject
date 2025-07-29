using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands;
using UserService.Queries;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
            => Ok(await _mediator.Send(new GetUserQuery { Id = id }));
    }
}
