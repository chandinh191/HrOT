using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Commands.UpdateTodoList;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Update;

public record Employee_UpdateLeaveLogCommand : IRequest<string>
{
    public Guid Id { get; init; }
    //public Guid EmployeeId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int LeaveHours { get; init; }
    public string Reason { get; init; }
    //public LeaveLogStatus Status { get; init; }
}
public class Employee_UpdateLeaveLogCommandHandler : IRequestHandler<Employee_UpdateLeaveLogCommand, string>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(Employee_UpdateLeaveLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(LeaveLog), request.Id);
        } else if ( entity.IsDeleted == true )
        {
            return "Log nghỉ phép này đã bị xóa";
        }

        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.LeaveHours = request.LeaveHours;
        entity.Reason = request.Reason;
        entity.Status = LeaveLogStatus.Pending;


        await _context.SaveChangesAsync(cancellationToken);

        return "Cập nhật thành công";
    }
}
