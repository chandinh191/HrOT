using hrOT.Application.Allowances.Command.Update;
using hrOT.Application.Allowances.Queries;
using hrOT.Application.Allowances.Command.Create;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Allowances.Command.Delete;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Controllers.Allowance;
[Authorize(Policy = "manager")]
public class AllowanceController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AllowanceList>> GetList()
    {
        return await Mediator.Send(new GetListAllowanceQuery());
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateAllowanceCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return Ok("Thêm thất bại");
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateAllowanceCommand command)
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
    public async Task<ActionResult> Delete(Guid id, DeleteAllowanceCommand command)
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
