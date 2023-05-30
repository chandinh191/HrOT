/*using hrOT.Application.Departments.Commands.CreateDepartment;
using hrOT.Application.Departments.Commands.DeleteDepartment;
using hrOT.Application.Departments.Commands.UpdateDepartment;
using hrOT.Application.Departments.Queries.GetDepartment;
using hrOT.Application.Departments;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using hrOT.Application.Companys;
using hrOT.Application.Companys.Queries.GetCompany;
using hrOT.Application.Companys.Commands.CreateCompany;
using hrOT.Application.Companys.Commands.UpdateCompany;
using hrOT.Application.Companys.Commands.DeleteCompany;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUI.Controllers.Companys;


[Authorize(Policy = "manager")]
public class CompanyController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<CompanyDTO>>> Get()
    {
        return await Mediator.Send(new GetListCompanyQuery());
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCompanyCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateCompanyCommand command)
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
    public async Task<ActionResult> Delete(Guid id, DeleteCompanyCommand command)
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

*/