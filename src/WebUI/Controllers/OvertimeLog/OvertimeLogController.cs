using hrOT.Application.OvertimeLogs.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.OvertimeLogs.Commands.Update;
using hrOT.Application.OvertimeLogs.Commands.Delete;
using hrOT.Application.OvertimeLogs.Commands.Create;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.OvertimeLog;
[Route("api/[controller]")]
[ApiController]
public class OvertimeLogController : ApiControllerBase
{

    [HttpGet]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<OvertimeLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListOvertimeLogQuery());
    }
    [HttpPost]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult<Guid>> Create(Employee_CreateOvertimeLogCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("Staff/{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateOvertimeLogCommand command)
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
    [HttpPut("Employee/{id}")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> Update(Guid id, Employee_UpdateOvertimeLogCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");

        }
        catch (Exception ex)
        {
            return BadRequest("Cập nhật thất bại");
        }
    }

    
     [HttpDelete("{id}")]
    [Authorize(Policy = "ManagerOrStaff")]
    public async Task<ActionResult> Delete(Guid id, DeleteOvertimeLogCommand command)
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
