/*using hrOT.Application.CompanyContracts.Commands.Create;
using hrOT.Application.CompanyContracts.Commands.Delete;
using hrOT.Application.CompanyContracts.Commands.Update;
using hrOT.Application.CompanyContracts.Queries;
using hrOT.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.CompanyContracts;
public class CompanyContractController : ApiControllerBase
{
    [HttpGet("{EmployeeId}")]
    public async Task<ActionResult<List<CompanyContractDto>>> GetListByEmployeeId(Guid EmployeeId)
    {
        return await Mediator.Send(new GetListCompanyContractByEmployeeIdQuery(EmployeeId));
    }
    [HttpGet]
    public async Task<ActionResult<List<CompanyContractDto>>> GetList()
    {
        return await Mediator.Send(new GetListCompanyContractQuery());
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCompanyContractCommand command, IFormFile cvFile)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateCompanyContractCommand command, IFormFile cvFile)
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
    public async Task<ActionResult> Delete(Guid id, DeleteCompanyContractCommand command)
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