using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace hrOT.Application.TimeAttendanceLogs.Commands.Update;
public record UpdateTimeAttendanceLog : IRequest<string>;
public class UpdateTimeAttendanceLogHandler : IRequestHandler<UpdateTimeAttendanceLog, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateTimeAttendanceLogHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateTimeAttendanceLog request, CancellationToken cancellationToken)
    {
        //muốn tính chấm công phải dựa vào bảng ngày làm việc thực tế
        //lấy ra lịch làm việc 365 ngày
        var AnnualWorkingDays = await _context.AnnualWorkingDays
            .Where(x => x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        //lấy ra bảng lịch sử chấm công
        var TimeAttendanceLogs = await _context.TimeAttendanceLogs
            .Where(x => x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        if(AnnualWorkingDays.Count == 0)
        {
            return "Vui lòng cập nhật danh sách ngày làm việc hàng năm";
        }
        if (TimeAttendanceLogs.Count == 0)
        {
            return "Vui lòng cập nhật danh sách lịch sử chấm công của nhân viên";
        }
        foreach (var log in TimeAttendanceLogs)
        {
            //tính số giờ làm việc trong 1 ngày
            //nếu nó đi làm trước 12h trưa tức là nó sẽ có nghỉ ăn cơm trưa thì phải trừ 1 tiếng đi nếu k tính cả thời gian nó nghỉ ăn cơm trưa công ty lỗ 1 tiếng
            var logDuration = (log.EndTime - log.StartTime).TotalHours;
            if (log.StartTime.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                logDuration -= 1;
            }
            log.Ducation = logDuration;
            //kiểm tra xem phải ngày nghỉ không
            var isHoliday = AnnualWorkingDays.Any(d => d.Day == log.StartTime.Date && (d.TypeDate == TypeDate.Holiday));
            //kiểm tra xem phải cuối tuần không
            var isWeekend = AnnualWorkingDays.Any(d => d.Day == log.StartTime.Date && (d.TypeDate == TypeDate.Weekend));

            //nếu là ngày lễ
            if(isHoliday) 
            {
                //đi làm vào ngày lễ thì tính là tăng ca
                var entity = new OvertimeLog();
                entity.EmployeeId = log.EmployeeId;
                entity.StartDate = log.StartTime.Date;
                entity.EndDate = log.EndTime.Date;
                entity.Coefficients = 2;
                entity.TotalHours = log.Ducation;
                entity.Status = OvertimeLogStatus.Approved;
                entity.CreatedBy = "Admin";
                entity.LastModified = DateTime.Now;
                entity.LastModifiedBy = "Admin";
                _context.OvertimeLogs.Add(entity);
            }
            //nếu là ngày cuối tuần
            if (isWeekend)
            {
                //đi làm vào ngày cuối tuần thì tính là tăng ca
                var entity = new OvertimeLog();
                entity.EmployeeId = log.EmployeeId;
                entity.StartDate = log.StartTime.Date;
                entity.EndDate = log.EndTime.Date;
                entity.Coefficients = 1.5;
                entity.TotalHours = log.Ducation;
                entity.Status = OvertimeLogStatus.Approved;
                entity.CreatedBy = "Admin";
                entity.LastModified = DateTime.Now;
                entity.LastModifiedBy = "Admin";
                _context.OvertimeLogs.Add(entity);
            }
            //nếu là ngày thường
            if(!isHoliday && !isWeekend)
            {
                // làm ít hơn 8 tiếng 1 ngày là sẽ lưu số giờ bị thiếu đó vào leavelog
                if (log.Ducation < 8)
                {
                    var entity = new LeaveLog();
                    entity.EmployeeId = log.EmployeeId;
                    entity.StartDate = log.StartTime.Date;
                    entity.EndDate = log.EndTime.Date;
                    entity.LeaveHours = (int)(8 - log.Ducation);
                    entity.Reason = "Đi làm trễ";
                    entity.Status = LeaveLogStatus.Approved;
                    entity.CreatedBy = "Admin";
                    entity.LastModified = DateTime.Now;
                    entity.LastModifiedBy = "Admin";
                    _context.LeaveLogs.Add(entity);
                }
                // làm nhiều hơn 8 tiếng 1 ngày sẽ lưu số giờ thừa ra vào overtimelog
                if (log.Ducation > 8)
                {
                    var entity = new OvertimeLog();
                    entity.EmployeeId = log.EmployeeId;
                    entity.StartDate = log.StartTime.Date;
                    entity.EndDate = log.EndTime.Date;
                    entity.Coefficients = 1.5;
                    entity.TotalHours = log.Ducation - 8;
                    entity.Status = OvertimeLogStatus.Approved;
                    entity.CreatedBy = "Admin";
                    entity.LastModified = DateTime.Now;
                    entity.LastModifiedBy = "Admin";
                    _context.OvertimeLogs.Add(entity);
                }
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return "Tính chấm công thành công";
    }
}
