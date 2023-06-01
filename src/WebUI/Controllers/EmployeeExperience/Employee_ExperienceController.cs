using hrOT.Application.EmployeeExperience.Commands;
using hrOT.Application.EmployeeExperience.Commands.Add;
using hrOT.Application.EmployeeExperience.Commands.Delete;
using hrOT.Application.Experiences.Commands;
using hrOT.Application.Experiences.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeExperience;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "ManagerOrStaff")]
public class Employee_ExperienceController : ApiControllerBase
{
    // Xuất danh sách
    [HttpGet("GetListExperience")]
    public async Task<IActionResult> GetListExperience()
    {
        var result = await Mediator
            .Send(new Employee_GetListExperienceQuery());

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Danh sách trống!");
    }

    // Khởi tạo
    [HttpPost("CreateExperience")]
    public async Task<IActionResult> CreateExperience([FromForm] ExperienceCommandDTO experienceDTO)
    {
        var result = await Mediator
            .Send(new Employee_ExperienceCreateCommand(experienceDTO));

        return Ok(result);
    }

    // Update
    [HttpPut("UpdateExperience")]
    public async Task<IActionResult> UpdateExperience(Guid experienceID, [FromForm] ExperienceCommandDTO experienceDTO)
    {
        //if (EmployeeID == Guid.Empty)
        //{
        //    return BadRequest("Vui lòng nhập EmployeeId !");
        //}

        if (experienceID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceUpdateCommand(experienceID, experienceDTO));

        return Ok(result);
    }

    // Xóa
    [HttpDelete("DeleteExperience")]
    public async Task<IActionResult> DeleteExperience(Guid experienceID)
    {
        if (experienceID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceDeleteCommand(experienceID));

        return Ok(result);
    }
}