using EmployeeWriteService.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWriteService.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] AddEmployeeCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
    }
}
