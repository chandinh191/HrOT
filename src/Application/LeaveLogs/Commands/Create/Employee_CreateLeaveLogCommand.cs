using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Commands.CreateTodoList;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Create;
public record Employee_CreateLeaveLogCommand : IRequest<Guid>
{
    public Guid EmployeeId { get; init; }
    public int LeaveHours { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string Reason { get; init; }
}


public class Employee_CreateLeaveLogCommandHandler : IRequestHandler<Employee_CreateLeaveLogCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(Employee_CreateLeaveLogCommand request, CancellationToken cancellationToken)
    {
        TimeSpan duration = request.EndDate - request.StartDate;
        int leaveDays = duration.Days + 1;
        int leaveHours = leaveDays * 8;
        var entity = new LeaveLog();
        entity.EmployeeId = request.EmployeeId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = request.LeaveHours;
        entity.Reason = request.Reason;
        entity.Status = LeaveLogStatus.Pending;
        entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Employee";

        _context.LeaveLogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}