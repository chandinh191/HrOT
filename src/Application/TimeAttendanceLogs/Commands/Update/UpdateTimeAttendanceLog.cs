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
        var TimeAttendanceLogs = await _context.TimeAttendanceLogs
            .Where(x => x.IsDeleted == false)
            .ToListAsync(cancellationToken);
        
        foreach (var log in TimeAttendanceLogs)
        {
            if(log.IsDeleted == true) 
            {
                return "Bảng thời gian biểu này đã bị xóa";
            }
            if (log.StartTime.TimeOfDay < new TimeSpan(12, 0, 0))
            {
                log.Ducation = (log.EndTime - log.StartTime).TotalHours - 1;
            }
            else
            {
                log.Ducation = (log.EndTime - log.StartTime).TotalHours;
            }
            if(log.Ducation < 8)
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
            if(log.Ducation > 8)
            {
                var entity = new OvertimeLog();
                entity.EmployeeId = log.EmployeeId;
                entity.StartDate = log.StartTime.Date;
                entity.EndDate = log.EndTime.Date;
                entity.TotalHours = log.Ducation - 8;
                entity.Status = OvertimeLogStatus.Approved;
                entity.CreatedBy = "Admin";
                entity.LastModified = DateTime.Now;
                entity.LastModifiedBy = "Admin";
                _context.OvertimeLogs.Add(entity);
            }
        }
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}
