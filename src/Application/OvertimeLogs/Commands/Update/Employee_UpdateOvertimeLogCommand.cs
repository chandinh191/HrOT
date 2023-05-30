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

namespace hrOT.Application.OvertimeLogs.Commands.Update;

public record Employee_UpdateOvertimeLogCommand : IRequest
{
    public Guid Id { get; init; }
    //public Guid EmployeeId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    public double TotalHours { get; init; }
}
public class Employee_UpdateOvertimeLogCommandHandler : IRequestHandler<Employee_UpdateOvertimeLogCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Employee_UpdateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }

        entity.Status = OvertimeLogStatus.Pending;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
        entity.TotalHours = request.TotalHours;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
