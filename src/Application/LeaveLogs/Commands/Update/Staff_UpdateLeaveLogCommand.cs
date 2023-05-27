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

public record Staff_UpdateLeaveLogCommand : IRequest
{
    public Guid Id { get; init; }
    public LeaveLogStatus Status { get; init; }
}
public class Staff_UpdateLeaveLogCommandHandler : IRequestHandler<Staff_UpdateLeaveLogCommand>
{
    private readonly IApplicationDbContext _context;

    public Staff_UpdateLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(Staff_UpdateLeaveLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(LeaveLog), request.Id);
        }

        entity.Status = request.Status;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Employee";


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
