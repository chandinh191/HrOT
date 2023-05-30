using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Companys.Commands.CreateCompany;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using OfficeOpenXml;
using System.Globalization;

namespace hrOT.Application.AnnualWorkingDays.Queries.Create;
public class CreateAnnualWorkingDayEx : IRequest<Guid>
{
    public string FilePath { get; set; }
}

public class CreateAnnualWorkingDayExCommandHandler : IRequestHandler<CreateAnnualWorkingDayEx, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAnnualWorkingDayExCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAnnualWorkingDayEx request, CancellationToken cancellationToken)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage(new FileInfo(request.FilePath)))
        {
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();
            if (worksheet == null)
            {
                throw new Exception("Invalid Excel file.");
            }

            var rowCount = worksheet.Dimension.Rows;
            var annualWorkingDays = new List<AnnualWorkingDay>();

            for (int row = 2; row <= rowCount; row++) // Bắt đầu từ hàng thứ 2 để bỏ qua tiêu đề
            {
                var dayCell = worksheet.Cells[row, 1].Value;

                if (dayCell != null)
                {
                    DateTime day;

                    if (DateTime.TryParse(dayCell.ToString(), out day))
                    {
                        if (_context.AnnualWorkingDays.Any(d => d.Day == day))
                        {
                            continue; // Skip this row and move to the next one
                        }
                        double coefficients;
                        TypeDate typeDate;

                        // Tự động tính toán hệ số lương và loại ngày dựa trên ngày
                        //Ngày lễ
                        if (IsHoliday(day))
                        {
                            coefficients = 2;
                            typeDate = TypeDate.Holiday;
                        }
                        //Ngày nghĩ
                        else if (IsWeekend(day))
                        {
                            coefficients = 1.5;
                            typeDate = TypeDate.Weekend;
                        }
                        //Ngày thường
                        else
                        {
                            coefficients = 1;
                            typeDate = TypeDate.Weekday;
                        }

                        var entity = new AnnualWorkingDay
                        {
                            Day = day,
                            Coefficients = coefficients,
                            TypeDate = typeDate
                        };

                        annualWorkingDays.Add(entity);
                    }
                }
            }

            _context.AnnualWorkingDays.AddRange(annualWorkingDays);
            await _context.SaveChangesAsync(cancellationToken);

            return annualWorkingDays.FirstOrDefault()?.Id ?? Guid.Empty;
        }
    }
    private bool IsHoliday(DateTime date)
    {
        // Tết Dương lịch - nghỉ 1 ngày (ngày 1/1)
        if (date.Month == 1 && date.Day == 1)
        {
            return true;
        }

        // Tết Nguyên đán - nghỉ 5 ngày
        var tetNguyenDanStart = GetTetNguyenDanStart(date.Year);
        var tetNguyenDanEnd = tetNguyenDanStart.AddDays(7); // Nghỉ từ ngày 29 đến ngày 5 của năm âm lịch
        if (date.Date >= tetNguyenDanStart && date.Date <= tetNguyenDanEnd)
        {
            return true;
        }

        // Giỗ tổ Hùng Vương - nghỉ 1 ngày (ngày 10/3 âm lịch)
        var gioToHungVuong = GetGioToHungVuong(date.Year);
        if (date.Month == gioToHungVuong.Month && date.Day == gioToHungVuong.Day)
        {
            return true;
        }

        // Chiến thắng 30/4 - nghỉ 1 ngày
        if (date.Month == 4 && date.Day == 30)
        {
            return true;
        }

        // Quốc tế lao động 1/5 - nghỉ 1 ngày
        if (date.Month == 5 && date.Day == 1)
        {
            return true;
        }

        // Quốc khánh 2/9 - nghỉ 2 ngày (ngày 2/9 và 1 ngày liền kề trước hoặc sau)
        if (date.Month == 9 && (date.Day == 2 || date.Day == 1 || date.Day == 3))
        {
            return true;
        }

        return false;
    }

    private DateTime GetTetNguyenDanStart(int year)
    {
        var lunarCalendar = new ChineseLunisolarCalendar();
        var lunarDate = lunarCalendar.ToDateTime(year-1, 12, 29, 0, 0, 0, 0); // Ngày 29 tháng 12 âm lịch
        
        return lunarDate;
    }



    private DateTime GetGioToHungVuong(int year)
    {
        var lunarCalendar = new ChineseLunisolarCalendar();
        var leapMonth = lunarCalendar.GetLeapMonth(year); // Lấy tháng nhuận trong năm âm lịch
        var month = 3;

        // Nếu năm nhuận và tháng nhuận là tháng 3, ta cần điều chỉnh sang tháng 4
        if (leapMonth == 3 && month >= leapMonth)
        {
            month++;
        }

        var lunarDate = lunarCalendar.ToDateTime(year, month, 10, 0, 0, 0, 0);
        return lunarDate;
    }



    private bool IsWeekend(DateTime date)
    {
        // Kiểm tra xem ngày có là cuối tuần hay không (thứ 7 hoặc Chủ nhật)
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

}
