using hrOT.Application.Departments;
using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments.Queries.GetTotalEmployees;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Departments;

[Authorize(Policy = "manager")]
public class DepartmentController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DepartmentDTO>>> Get()
    {
        return await Mediator.Send(new GetListDepartmentQuery());
    }

    [HttpGet("GetTotalEmployeeInDepartment")]
    public async Task<IActionResult> GetEmployeeInDepartment(Guid DepartmentId)
    {
        var result = await Mediator.Send(new GetListEmployeeInDepartmentQuery(DepartmentId));
        return result > 0
            ? Ok(result)
            : BadRequest($"Không tồn tại nhân viên nào trong phòng ban ID: {DepartmentId}");
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateDepartmentCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateDepartmentCommand command)
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
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await Mediator.Send(new DeteleDepartmentCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return BadRequest("Xóa thất bại");
        }
    }
}