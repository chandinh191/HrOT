using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.Holidays;
using hrOT.Application.Holidays.Queries.GetHoliday;
using hrOT.Application.Holidays.Commands.CreateHoliday;
using hrOT.Application.Holidays.Commands.UpdateHoliday;
using hrOT.Application.Holidays.Commands.DeleteHoliday;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUI.Controllers.Holidays;


[Authorize(Policy = "manager")]
public class HolidayController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<HolidayDTO>>> Get()
    {
        return await Mediator.Send(new GetListHolidayQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateHolidayCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateHolidayCommand command)
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
    public async Task<ActionResult> Delete(Guid id, DeleteHolidayCommand command)
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
