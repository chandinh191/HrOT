using hrOT.Application.LeaveLogs.Queries;

using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Application.LeaveLogs.Commands.Create;
using hrOT.Application.LeaveLogs.Commands.Delete;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.LeaveLog;

public class LeaveLogController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<LeaveLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListLeaveLogQuery());
    }
    [HttpPost]
    [Authorize(Policy = "employee")]
    public async Task<ActionResult<Guid>> Create(Employee_CreateLeaveLogCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }
    [HttpPut("Staff/{id}")]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateLeaveLogCommand command)
    {
        if (id != command.Id)
        {
            return Ok("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");

        }
        catch (Exception ex)
        {
            return Ok("Cập nhật thất bại");
        }
    }
    [HttpPut("Employee/{id}")]
    [Authorize(Policy = "employee")]
    public async Task<ActionResult> Update(Guid id, Employee_UpdateLeaveLogCommand command)
    {
        if (id != command.Id)
        {
            return Ok("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Cập nhật thành công");

        }
        catch (Exception ex)
        {
            return Ok("Cập nhật thất bại");
        }
    }
    

    [HttpDelete("{id}")]
    [Authorize(Policy = "employee")]
    public async Task<ActionResult> Delete(Guid id, DeleteLeaveLogCommand command)
    {
        if (id != command.Id)
        {
            return Ok("Lỗi! Không tìm thấy Id");
        }
        try
        {
            await Mediator.Send(command);
            return Ok("Xóa thành công");

        }
        catch (Exception ex)
        {
            return Ok("Xóa thất bại");
        }
    }
}
