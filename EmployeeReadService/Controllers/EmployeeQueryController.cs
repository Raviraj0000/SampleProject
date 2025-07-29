using EmployeeReadService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeReadService.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeQueryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeQueryController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _mediator.Send(new GetEmployeeQuery { Id = id }));
    }
}
