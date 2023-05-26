using hrOT.Application.Common.Exceptions;
using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Delete;
using hrOT.Application.Employees.Commands.Update;
using hrOT.Application.Employees.Queries;
using LogOT.Application.Employees.Commands.Create;
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, [FromForm] UpdateEmployee command, [FromForm] IFormFile image)
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
                return Ok("Cập nhật thất bại");
            }
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(Guid id, DeleteEmployee command)
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
        public async Task<IActionResult> CreateEx( [FromForm] CreateEmployeeEx createModel)
        {
            if (ModelState.IsValid && createModel != null)
            {
                var command = new CreateEmployeeEx
                {
                    FilePath = "E:\\ASP.NET\\LogOTAPI\\src\\WebUI\\wwwroot\\Ex/employees.xlsx"
                };

                await _mediator.Send(command);


                return Ok("Thêm thành công");
            }


            return BadRequest();
        }

        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            try
            {
                var query = new Employee_GetEmployeeQuery { Id = id };
                var employeeVm = await _mediator.Send(query);

                if (employeeVm == null)
                {
                    return NotFound("Employee not found");
                }

                return Ok(employeeVm);
            }
            catch (NotFoundException)
            {
                return NotFound("Employee not found");
            }
        }

        [HttpPost("{id}/cv")]
        public async Task<IActionResult> UploadCV(Guid id, IFormFile cvFile)
        {
            try
            {
                if (cvFile == null || cvFile.Length == 0)
                {
                    return BadRequest("No file uploaded");
                }

                var command = new Employee_EmployeeUploadCVCommand
                {
                    Id = id,
                    CVFile = cvFile
                };

                await _mediator.Send(command);

                return Ok("CV uploaded successfully");
            }
            catch (Exception ex)
            {
                // Handle and log the exception
                return StatusCode(500, "An error occurred while uploading the CV");
            }
        }



    }
}
