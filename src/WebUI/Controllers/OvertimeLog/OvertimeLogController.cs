﻿using hrOT.Application.OvertimeLogs.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.OvertimeLogs.Commands.Update;
using hrOT.Application.OvertimeLogs.Commands.Delete;
using hrOT.Application.OvertimeLogs.Commands.Create;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.OvertimeLog;
public class OvertimeLogController : ApiControllerBase
{

    [HttpGet]
    [Authorize(Policy = "manager")]
    public async Task<ActionResult<OvertimeLogList>> GetList()
    {
        return await Mediator.Send(new Staff_GetListOvertimeLogQuery());
    }
    [HttpPost]
    [Authorize(Policy = "employee")]
    public async Task<ActionResult<Guid>> Create(Employee_CreateOvertimeLogCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }
    [HttpPut("Staff/{id}")]
    [Authorize(Policy = "staff")]
    public async Task<ActionResult> UpdateStatus(Guid id, Staff_UpdateOvertimeLogCommand command)
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
    public async Task<ActionResult> Update(Guid id, Employee_UpdateOvertimeLogCommand command)
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
    public async Task<ActionResult> Delete(Guid id, DeleteOvertimeLogCommand command)
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
