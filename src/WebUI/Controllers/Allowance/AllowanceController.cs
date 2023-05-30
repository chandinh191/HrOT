using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.Allowances.Command.Create;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Allowances.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.Allowance;

public class AllowanceController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult<AllowanceList>> GetList()
    {
        return await Mediator.Send(new GetListAllowanceQuery());
    }
    [HttpPost]
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
    [HttpPut("{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> Update(Guid id, UpdateAllowanceCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            var result = await Mediator.Send(command);
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }
    
    [HttpDelete("{id}")]
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
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}
