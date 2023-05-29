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

namespace hrOT.Application.InterviewProcesses.Commands.Delete;
public record DeleteInterviewProcessCommand(Guid Id) : IRequest;

public class DeleteInterviewProcessCommandHandler : IRequestHandler<DeleteInterviewProcessCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteInterviewProcessCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteInterviewProcessCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.InterviewProcesses
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(InterviewProcess), request.Id);
        }

        entity.IsDeleted = true;
        entity.LastModified = DateTime.Now;
        entity.LastModifiedBy = "Admin";

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
