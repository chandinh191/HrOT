/*using hrOT.Application.OvertimeLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using hrOT.Application.Skill_JDs.Queries;
using hrOT.WebUI.Controllers;
using hrOT.Application.Skill_JDs.Commands.Create;
using hrOT.Application.Skill_JDs.Commands.Update;
using hrOT.Application.Skill_JDs.Commands.Delete;

namespace WebUI.Controllers.Skill_JD;
public class Skill_JDController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Skill_JDList>> GetList()
    {
        return await Mediator.Send(new GetSkill_JDListQuery());
    }
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateSkill_JDCommand command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Thêm thành công");
        }
        return BadRequest("Thêm thất bại");
    }
    [HttpPut("UpdateSkill_JD/{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateSkill_JDCommand command)
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
    public async Task<ActionResult> Delete(Guid id, DeleteSkill_JDCommand command)
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