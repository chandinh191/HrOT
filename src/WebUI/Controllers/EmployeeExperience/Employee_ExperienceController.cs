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
    public async Task<IActionResult> GetListExperience(Guid EmployeeID)
    {
        if (EmployeeID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListExperienceQuery(EmployeeID));

        return result.Count > 0
            ? Ok(result)
            : BadRequest("Id nhân viên không tồn tại!");
    }

    // Khởi tạo
    [HttpPost("CreateExperience")]
    public async Task<IActionResult> CreateExperience(Guid EmployeeID, [FromForm] ExperienceCommandDTO experienceDTO)
    {
        if (EmployeeID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceCreateCommand(experienceDTO, EmployeeID));

        return Ok(result);
    }

    // Update
    [HttpPut("UpdateExperience")]
    public async Task<IActionResult> UpdateExperience(Guid ExperienceID, Guid EmployeeID, [FromForm] ExperienceCommandDTO experienceDTO)
    {
        if (EmployeeID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (ExperienceID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceUpdateCommand(ExperienceID, EmployeeID, experienceDTO));

        return Ok(result);
    }

    // Xóa
    [HttpDelete("DeleteExperience")]
    public async Task<IActionResult> DeleteExperience(Guid ExperienceID, Guid EmployeeID)
    {
        if (EmployeeID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (ExperienceID == Guid.Empty)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceDeleteCommand(ExperienceID, EmployeeID));

        return Ok(result);
    }
}