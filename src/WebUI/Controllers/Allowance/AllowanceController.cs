using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.Allowances.Command.Create;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Allowances.Command.Delete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Degrees.Queries;
using hrOT.Application.Degrees;

namespace WebUI.Controllers.Allowance;

public class AllowanceController : ApiControllerBase
{
    [HttpGet("GetAllAllowance")]
    [Authorize(Policy = "Manager")]
    public async Task<List<AllowanceDto>> GetList()
    {
        return await Mediator.Send(new GetListAllowanceQuery());
    }

    [HttpGet("GetListByEmployeeId")]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult<List<AllowanceDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListAllowanceByEmployeeIdQuery(EmployeeId));
    }


    [HttpPost("Create")]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult<Guid>> Create(CreateAllowanceCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("Update")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Update([FromForm]UpdateAllowanceCommand command)
    {
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);

        }
        catch (NotFoundException ex)
        {
            return NotFound("Cập nhật thất bại");
        }
    }
    
    [HttpDelete("Delete{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Delete(Guid id, DeleteAllowanceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Xóa thành công");

        }
        catch (NotFoundException ex)
        {
            return NotFound("Xóa thất bại");
        }
    }
}
