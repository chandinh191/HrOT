using hrOT.Application.Employees_Skill;
using hrOT.Application.Employees_Skill.Queries;
using hrOT.Application.EmployeeSkill.Commands;
using hrOT.Application.EmployeeSkill.Commands.Add;
using hrOT.Application.EmployeeSkill.Commands.Delete;
using hrOT.Domain.Entities;
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
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListSkillQuery(EmployeeID));

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Không tìm thấy bất kì kĩ năng bản thân nào");
    }

    [HttpPost("CreateSkill")]
    public async Task<IActionResult> CreateSkill(Guid EmployeeId, [FromForm] Skills_EmployeeCommandDTO skill_Employee, string SkillName)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }
        else if (SkillName == null || SkillName == "") 
        {
            return BadRequest("Vui lòng nhập SkillName !");
        }
        var result = await Mediator
           .Send(new Employee_CreateSkillCommand(EmployeeId, SkillName, skill_Employee));

        if (result == true)
        {
            return Ok("Thêm thành công");
        }
        return BadRequest("Không tìm thấy EmployeeID");
    }

       

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid EmployeeId, string SkillName)
    {
        if (EmployeeId.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (SkillName == null || SkillName == "")
        {
            return BadRequest("Vui lòng nhập SkillId !");
        }

        var result = await Mediator
            .Send(new Employee_DeleteSkillCommand(EmployeeId, SkillName));

        if (result == true)
        {
            return Ok("Xóa thành công");
        }
        return BadRequest("Không tìm thấy kĩ năng bản thân cần xóa");
    }
}