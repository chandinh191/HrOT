﻿using hrOT.Application.Employees.Commands.Create;
using hrOT.Application.Employees.Commands.Delete;
using hrOT.Application.Employees.Commands.Update;
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


    }
}
