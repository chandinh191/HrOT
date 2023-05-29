using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.OvertimeLogs.Commands.Delete;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Skill_JDs.Commands.Delete;
public class DeleteSkill_JDCommand : IRequest
{
    public Guid Id { get; init; }
}

public class DeleteSkill_JDCommandHandler : IRequestHandler<DeleteSkill_JDCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteSkill_JDCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSkill_JDCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skill_JDs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill_JD), request.Id);
        }

        entity.IsDeleted = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
