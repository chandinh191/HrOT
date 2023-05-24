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

public record Staff_UpdateOvertimeLogCommand : IRequest
{
    public Guid Id { get; init; }
    //public Guid EmployeeId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }
    public OvertimeLogStatus Status { get; init; }
}
public class Staff_UpdateOvertimeLogCommandHandler : IRequestHandler<Staff_UpdateOvertimeLogCommand>
{
    private readonly IApplicationDbContext _context;

    public Staff_UpdateOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Staff_UpdateOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }

        entity.Status = request.Status;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
