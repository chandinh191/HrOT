using hrOT.Application.LeaveLogs.Queries;

using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.LeaveLogs.Commands.Update;
using hrOT.Application.LeaveLogs.Commands.Create;
using hrOT.Application.LeaveLogs.Commands.Delete;

namespace WebUI.Controllers.LeaveLog;
public class LeaveLogController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<LeaveLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListLeaveLogQuery());
    }
    [HttpPost]
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
