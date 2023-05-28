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

namespace hrOT.Application.OvertimeLogs.Commands.Delete;

public record DeleteOvertimeLogCommand : IRequest
{
    public Guid Id { get; init; }
    //public bool IsDeleted { get; init; }

}
public class DeleteOvertimeLogCommandHandler : IRequestHandler<DeleteOvertimeLogCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteOvertimeLogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteOvertimeLogCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OvertimeLogs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OvertimeLog), request.Id);
        }

        entity.IsDeleted = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}