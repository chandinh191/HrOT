using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Skill_JDs.Commands.Update;
public record UpdateSkill_JDCommand : IRequest
{
    public Guid Id { get; init; }
    public Guid SkillId { get; init; }
    public Guid JobDescriptionId { get; init; }
    public string Level { get; init; }
}

public class UpdateSkill_JDCommandHandler : IRequestHandler<UpdateSkill_JDCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateSkill_JDCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateSkill_JDCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Skill_JDs
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Skill_JD), request.Id);
        }
        entity.SkillId= request.SkillId;
        entity.JobDescriptionId= request.JobDescriptionId;
        entity.Level= request.Level;
        


        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
