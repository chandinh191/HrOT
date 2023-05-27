﻿using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;

using hrOT.Domain.Entities;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments;
using hrOT.Application.Exchanges.Commands.DeleteExchange;

namespace WebUI.Controllers.Departments;
[Authorize]
public class DepartmentController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> Get()
    {
        return await Mediator.Send(new GetListDepartmentQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateDepartmentCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }
    

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand command)
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
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeteleDepartmentCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return Ok("Xóa thất bại");
        }

    }
}
