using LogOT.Application.Employees.Commands.Create;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployee createModel)
        {
            if (ModelState.IsValid && createModel != null)
            {
                var entityId = await _mediator.Send(createModel);
               
                return Ok("Thêm thành công");
            }

            return BadRequest();
        }
    }
}
