using hrOT.Application.EmployeeExperience.Commands.Add;
using hrOT.Application.EmployeeExperience.Commands.Delete;
using hrOT.Application.Experiences;
using hrOT.Application.Experiences.Commands;
using hrOT.Application.Experiences.Queries;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.EmployeeExperience;

[Route("api/[controller]")]
[ApiController]
public class Employee_ExperienceController : ApiControllerBase
{

    // Xuất danh sách
    [HttpGet("GetListKinhNghiem")]
    public async Task<IActionResult> GetListKinhNghiem(Guid EmployeeID)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_GetListExperienceQuery(EmployeeID));

        if (result != null)
        {
            return Ok(result);
        }
        return BadRequest($"Không tìm thấy bất kì kinh nghiệm bản thân nào của EmployeeID: {EmployeeID}");
    }


    // Khởi tạo
    [HttpPost("TaoKinhNghiem")]
    public async Task<IActionResult> TaoKinhNghiem(Guid EmployeeID, ExperienceDTO experienceDTO)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceCreateCommand(experienceDTO, EmployeeID));

        if (result == true)
        {
            return Ok($"Thêm thành công kinh nghiệm bản thân cho EmployeeID: {EmployeeID}");
        }
        return BadRequest($"Không tìm thấy EmployeeID: {EmployeeID}");
    }


    // Update
    [HttpPut("UpdateKinhNghiem")]
    public async Task<IActionResult> UpdateKinhNghiem(Guid ExperienceID, Guid EmployeeID, ExperienceDTO experienceDTO)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (ExperienceID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceUpdateCommand(ExperienceID, EmployeeID, experienceDTO));

        if (result == true)
        {
            return Ok($"Cập nhật thành công kinh nghiệm bản thân cho EmployeeID: {EmployeeID}");
        }
        return BadRequest($"Không tìm thấy kinh nghiệm với : \nEmployeeID: {EmployeeID}\n ExperienceID: {ExperienceID}");
    }

    // Xóa
    [HttpDelete("XoaKinhNghiem")]
    public async Task<IActionResult> XoaKinhNghiem(Guid ExperienceID, Guid EmployeeID)
    {
        if (EmployeeID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập EmployeeId !");
        }

        if (ExperienceID.ToString() == null)
        {
            return BadRequest("Vui lòng nhập ExperienceID !");
        }

        var result = await Mediator
            .Send(new Employee_ExperienceDeleteCommand(ExperienceID, EmployeeID));

        if (result == true)
        {
            return Ok($"Xóa thành công kinh nghiệm bản thân cho EmployeeID: {EmployeeID}");
        }
        return BadRequest($"Không tìm thấy kinh nghiệm với : \nEmployeeID: {EmployeeID}\n ExperienceID: {ExperienceID}");
    }
}