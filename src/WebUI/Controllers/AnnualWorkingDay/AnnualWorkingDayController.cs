using hrOT.Application.Allowances.Command.Create;
using hrOT.Application.AnnualWorkingDays.Queries.Create;
using hrOT.WebUI.Controllers;
using LogOT.Application.Employees.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.AnnualWorkingDay;
public class AnnualWorkingDayController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public AnnualWorkingDayController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("CreateEx")]
    public async Task<IActionResult> CreateEx(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // Kiểm tra kiểu tệp tin
            if (!IsExcelFile(file))
            {
                return BadRequest("Bạn phải import bằng file Excel");
            }

            var filePath = Path.GetTempFileName(); // Tạo một tệp tạm để lưu trữ tệp Excel
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Lưu tệp Excel vào tệp tạm
            }

            var command = new CreateAnnualWorkingDayEx
            {
                FilePath = filePath
            };

            await _mediator.Send(command);

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

}
