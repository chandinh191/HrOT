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
    public async Task<IActionResult> GetListSKill()
    {
        var result = await Mediator
            .Send(new Employee_GetListSkillQuery());

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Danh sách trống!");
    }

    [HttpPost("AddSkill")]
    public async Task<IActionResult> AddSkill([FromForm] Skills_EmployeeCommandDTO skill_Employee, Guid SkillId)
    {
        if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillName !");
        }

        var result = await Mediator
           .Send(new Employee_CreateSkillCommand(SkillId, skill_Employee));

        return Ok(result);
    }

    [HttpPut("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill(Guid SkillId, [FromForm] Skills_EmployeeCommandDTO skills_Employee)
    {
        if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_UpdateSkillCommand(SkillId, skills_Employee));
        return Ok(result);
    }

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid SkillId)
    {
        if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_DeleteSkillCommand(SkillId));

        return Ok(result);
    }
}