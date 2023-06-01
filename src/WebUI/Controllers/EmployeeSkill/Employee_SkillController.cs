using hrOT.Application.Employees_Skill.Queries;
using hrOT.Application.EmployeeSkill.Commands;
using hrOT.Application.EmployeeSkill.Commands.Add;
using hrOT.Application.EmployeeSkill.Commands.Delete;
using hrOT.Application.EmployeeSkill.Commands.Update;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeSkill;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "ManagerOrStaff")]
public class Employee_SkillController : ApiControllerBase
{
    [HttpGet("GetListSKill")]
    public async Task<IActionResult> GetListSKill(Guid EmployeeID)
    {
        if (EmployeeID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListSkillQuery(EmployeeID));

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Id nhân viên không tồn tại!");
    }

    [HttpPost("AddSkill")]
    public async Task<IActionResult> AddSkill(Guid EmployeeId, [FromForm] Skills_EmployeeCommandDTO skill_Employee, Guid SkillId)
    {
        if (EmployeeId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }
        else if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillName !");
        }

        var result = await Mediator
           .Send(new Employee_CreateSkillCommand(EmployeeId, SkillId, skill_Employee));

        return Ok(result);
    }

    [HttpPut("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill(Guid EmployeeId, Guid SkillId, [FromForm] Skills_EmployeeCommandDTO skills_Employee)
    {
        if (EmployeeId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }
        else if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_UpdateSkillCommand(EmployeeId, SkillId, skills_Employee));
        return Ok(result);
    }

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid EmployeeId, Guid SkillId)
    {
        if (EmployeeId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_DeleteSkillCommand(EmployeeId, SkillId));

        return Ok(result);
    }
}