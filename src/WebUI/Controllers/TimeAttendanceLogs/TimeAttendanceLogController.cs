﻿using hrOT.Application.TimeAttendanceLogs.Commands.Create;
using hrOT.Application.TimeAttendanceLogs.Commands.Update;
using hrOT.WebUI.Controllers;
using LogOT.Application.Employees.Commands.Create;
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

    [HttpPost("CalculatorDucation")]
    [Authorize(Policy = "manager")]
    public async Task<IActionResult> CalculatorDucation(UpdateTimeAttendanceLog command)
    {
        if (ModelState.IsValid && command != null)
        {
            await Mediator.Send(command);
            return Ok("Tính chấm công thành công");
        }
        return BadRequest("Tính chấm công thất bại");
    }
}
