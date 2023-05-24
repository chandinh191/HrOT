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

namespace hrOT.Application.OvertimeLogs.Commands.Create;
public record Employee_CreateLeaveLogCommand : IRequest<Guid>
{
    //public Guid Id { get; init; }
    public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int LeaveHours { get; init; }
    public string Reason { get; init; }
    public LeaveLogStatus Status { get; init; }
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
        var entity = new LeaveLog();
        entity.EmployeeId = request.EmployeeId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = request.LeaveHours;
        entity.Reason = request.Reason;
        entity.Status = request.Status;

        _context.LeaveLogs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}