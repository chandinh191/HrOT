using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using LogOT.Application.Employees.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OfficeOpenXml;

namespace hrOT.Application.TimeAttendanceLogs.Commands.Create;
public record CreateTimeAttendanceLogByExcel : IRequest<string>
{
    public string FilePath { get; set; }
}
public class CreateTimeAttendanceLogByExcelHandler : IRequestHandler<CreateTimeAttendanceLogByExcel, string>
{
    private readonly IApplicationDbContext _context;

    public CreateTimeAttendanceLogByExcelHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(CreateTimeAttendanceLogByExcel request, CancellationToken cancellationToken)
    {
        var filePath = request.FilePath;
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The specified file does not exist.", filePath);
        }

        using (var package = new ExcelPackage(new FileInfo(filePath)))
        {
            var worksheet = package.Workbook.Worksheets[0];
            var rowCount = worksheet.Dimension.Rows;

            var timeAttendanceLog = new List<TimeAttendanceLog>();

            for (int row = 2; row <= rowCount; row++)
            {
                var employeeId = worksheet.Cells[row, 1].GetValue<string>();
                var startTime = worksheet.Cells[row, 2].GetValue<DateTime>();
                var endTime = worksheet.Cells[row, 3].GetValue<DateTime>();

                var log = new TimeAttendanceLog
                {
                    EmployeeId = Guid.Parse(employeeId),
                    StartTime = startTime,
                    EndTime = endTime
                };

                timeAttendanceLog.Add(log);
            }

            await _context.TimeAttendanceLogs.AddRangeAsync(timeAttendanceLog);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return "Thêm thành công";
    }

}
