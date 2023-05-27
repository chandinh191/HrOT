using hrOT.Application.Employees_Skill;
using hrOT.Application.Employees_Skill.Queries;
using hrOT.Application.EmployeeSkill.Commands.Add;
using hrOT.Application.EmployeeSkill.Commands.Delete;
using hrOT.Application.EmployeeSkill.Commands.Update;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeSkill;

[Route("api/[controller]")]
[ApiController]
public class Employee_SkillController : ApiControllerBase
{
    [HttpGet("GetListSKill")]
    public async Task<IActionResult> GetListSKill(Guid EmployeeID)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListSkillQuery(EmployeeID));

        return result.Count > 0 
            ? Ok(result) 
            : BadRequest($"Không tìm thấy bất kì kĩ năng bản thân nào của EmployeeID: {EmployeeID}");
    }

    [HttpPost("CreateSkill")]
    public async Task<IActionResult> CreateSkill(Guid EmployeeId, [FromForm] Skill_EmployeeDTO skill_Employee)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_CreateSkillCommand(EmployeeId, skill_Employee));
        if (result == true)
        {
            return Ok($"Thêm thành công kĩ năng bản thân cho EmployeeID: {EmployeeId}");
        }
        return BadRequest($"Không tìm thấy EmployeeID: {EmployeeId}");
    }

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid EmployeeId, Guid SkillId)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (SkillId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_DeleteSkillCommand(EmployeeId, SkillId));

        if (result == true)
        {
            return Ok($"Xóa thành công kĩ năng bản thân cho EmployeeID: {EmployeeId}");
        }
        return BadRequest($"Không tìm thấy kĩ năng bản thân cần xóa của EmployeeID: {EmployeeId}");
    }

    [HttpPut("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill(Guid EmployeeId, Guid SkillId, [FromForm] Skill_EmployeeDTO skill_EmployeeDTO)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (SkillId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_UpdateSkillCommand(EmployeeId, SkillId, skill_EmployeeDTO));

        if (result == true)
        {
            return Ok($"Cập nhập thành công kĩ năng bản thân cho EmployeeID: {EmployeeId}");
        }
        return BadRequest($"Không tìm thấy kĩ năng bản thân cần cần nhập của EmployeeID: {EmployeeId}");
    }
}