﻿using hrOT.Application.Skills;
using hrOT.Application.Skills.Commands;
using hrOT.Application.Skills.Commands.Add;
using hrOT.Application.Skills.Commands.Delete;
using hrOT.Application.Skills.Commands.Update;
using hrOT.Application.Skills.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Skills;
[Route("api/[controller]")]
[ApiController]
public class SkillsController : ApiControllerBase
{
    [HttpGet("GetSkillsList")]
    public async Task<IActionResult> GetSkillsList()
    {
        var result = await Mediator.Send(new GetSkillListQuery());
        
        return result != null 
            ? Ok(result) 
            : BadRequest("Danh sách list trống.");
    }

    [HttpPost("AddSkill")]
    public async Task<IActionResult> AddSkill([FromForm] SkillCommandDTO skillDTO)
    {
        var result = await Mediator.Send(new CreateSkillCommand(skillDTO));

        return result == true
            ? Ok(result)
            : BadRequest("Lỗi xảy ra, không thể thêm kĩ năng.");
    }

    [HttpPut("UpdateSkill")]
    public async Task<IActionResult> UpdateSkill(Guid SkillId, [FromForm] SkillCommandDTO skill)
    {
        if(SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập vào Id kĩ năng.");
        }
        var result = await Mediator.Send(new UpdateSkillCommand(SkillId, skill));

        return result == true
            ? Ok("Cập nhật kĩ năng thành công.")
            : BadRequest($"Cập nhật kĩ năng thất bại cho SkillId: {SkillId}");
    }

    [HttpDelete("DeleteSkill")]
    public async Task<IActionResult> DeleteSkill(Guid SkillId)
    {
        if (SkillId == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập vào Id kĩ năng.");
        }
        var result = await Mediator.Send(new DeleteSkillCommand(SkillId));

        return result == true
            ? Ok("Cập nhật kĩ năng thành công.")
            : BadRequest($"Cập nhật kĩ năng thất bại cho SkillId: {SkillId}");
    }
}
