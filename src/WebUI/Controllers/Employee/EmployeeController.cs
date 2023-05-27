using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Delete;
using hrOT.Application.Employees.Commands.Update;
using LogOT.Application.Employees;
using LogOT.Application.Employees.Commands.Create;
using LogOT.Application.Employees.Queries;
using MediatR;

using Microsoft.AspNetCore.Mvc;


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
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> Get()
        {
            return await _mediator.Send(new GetAllEmployeeQuery());
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateEmployee([FromForm] CreateEmployee createModel)
        {
            if (ModelState.IsValid && createModel != null)
            {
                var entityId = await _mediator.Send(createModel);
               
                return Ok("Thêm thành công");
            }


            var errorMessages = ModelState.Values
        .SelectMany(v => v.Errors)
        .Select(e => e.ErrorMessage)
        .ToList();

            return BadRequest(errorMessages);

           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateEmployee command)
        {
            if (id != command.Id)
            {
                return Ok("Không tìm thấy Id");
            }
            try
            {
                //command.Image = image; 

                await _mediator.Send(command);
                return Ok("Cập nhật thành công");
               
            }
            catch (Exception ex)
            {
                var errorMessages = ModelState.Values
       .SelectMany(v => v.Errors)
       .Select(e => e.ErrorMessage)
       .ToList();

                return BadRequest(errorMessages);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Delete(Guid id, [FromForm] DeleteEmployee command)

        {
            if (id != command.Id)
            {
                return Ok();
            }

            try
            {
                await _mediator.Send(command);
               
                return Ok("Xóa thành công");
            }
            catch (Exception ex)
            {
                
                
                return Ok("Xóa thất bại");
            }
        }
        [HttpPost("CreateEx")]
        public async Task<IActionResult> CreateEx( IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
                }

                var command = new CreateEmployeeEx
                {
                    FilePath = filePath
                };

                await _mediator.Send(command);

                return Ok("Thêm thành công");
            }

           


            return Ok("Thêm thất bại");
        }


    }
}
