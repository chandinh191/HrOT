using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Levels;
using hrOT.Application.Levels.Commands.CreateLevel;
using hrOT.Application.Levels.Commands.DeleteLevel;
using hrOT.Application.Levels.Commands.UpdateLevel;
using hrOT.Application.Levels.Queries.GetLevel;
using hrOT.Domain.Entities;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Levels;


[Authorize]
public class LevelController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<LevelDTO>>> Get()
    {
        return await Mediator.Send(new GetListLevelQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateLevelCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateLevelCommand command)
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
            await Mediator.Send(new DeleteLevelCommand(id));
            return Ok("Xóa thành công");
        }
        catch (Exception ex)
        {
            return Ok("Xóa thất bại");
        }

    }
}

