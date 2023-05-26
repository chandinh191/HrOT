using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.LeaveLogs.Commands.Delete;

public record DeleteLeaveLogCommand : IRequest
{
    public Guid Id { get; init; }
    //public bool IsDeleted { get; init; }
}
public class DeleteLeaveLogCommandHandler : IRequestHandler<DeleteLeaveLogCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteLeaveLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLeaveLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.LeaveLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(LeaveLog), request.Id);
        }

        entity.IsDeleted = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
