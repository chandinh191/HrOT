using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.OvertimeLogs.Commands.Create;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;

namespace hrOT.Application.Skill_JDs.Commands.Create;
public record CreateSkill_JDCommand : IRequest<Guid>
{
    public Guid SkillId { get; init; }
    public Guid JobDescriptionId { get; init; }
    public string Level { get; init; }
}

public class CreateSkill_JDCommandHandler : IRequestHandler<CreateSkill_JDCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSkill_JDCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSkill_JDCommand request, CancellationToken cancellationToken)
    {
        var entity = new Skill_JD();
        entity.Id = Guid.NewGuid();
        entity.SkillId = request.SkillId;
        entity.JobDescriptionId = request.JobDescriptionId;
        entity.Level = request.Level;
        //entity.CreatedBy = "Employee";
        entity.LastModified = DateTime.Now;
        //entity.LastModifiedBy = "Employee";

        _context.Skill_JDs.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}