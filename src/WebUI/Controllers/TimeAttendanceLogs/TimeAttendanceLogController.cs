using hrOT.Application.TimeAttendanceLogs.Commands.Create;
using hrOT.Application.TimeAttendanceLogs.Commands.Update;
using hrOT.WebUI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.TimeAttendanceLogs;

public class TimeAttendanceLogController : ApiControllerBase
{
    [HttpPost("ImportExcel")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> ImportExcel(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // Kiểm tra kiểu tệp tin
            if (!IsExcelFile(file))
            {
                return BadRequest("Chỉ cho phép sử dụng file Excel");
            }
            var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
            }

            var command = new CreateTimeAttendanceLogByExcel
            {
                FilePath = filePath
            };

            await Mediator.Send(command);

            return Ok("Thêm thành công");
        }

        return BadRequest("Thêm thất bại");
    }

    private bool IsExcelFile(IFormFile file)
    {
        // Kiểm tra phần mở rộng của tệp tin có phải là .xls hoặc .xlsx không
        var allowedExtensions = new[] { ".xls", ".xlsx" };
        var fileExtension = Path.GetExtension(file.FileName);
        return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }

    [HttpPost("CalculatorDucation")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> CalculatorDucation(UpdateTimeAttendanceLog command)
    {
        if (ModelState.IsValid && command != null)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        return BadRequest("Tính chấm công thất bại");
    }
}