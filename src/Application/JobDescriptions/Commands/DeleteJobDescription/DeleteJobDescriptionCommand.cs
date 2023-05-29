using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.JobDescriptions.Commands.DeleteJobDescription;

public record DeleteJobDescriptionCommand(Guid Id) : IRequest;

public class DeteleJobDescriptionCommandHandler : IRequestHandler<DeleteJobDescriptionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeteleJobDescriptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteJobDescriptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.JobDescriptions
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(JobDescription), request.Id);
        }

        entity.IsDeleted = true;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;

    }
}


