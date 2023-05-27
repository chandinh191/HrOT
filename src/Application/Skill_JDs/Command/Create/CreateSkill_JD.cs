/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees_Skill;
using hrOT.Application.EmployeeSkill.Commands.Add;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Skill_JDs.Command.Create;
public class CreateSkill_JD:IRequest<string>
{
    public Guid SkillId { get; set; }
    public Guid JobDescriptionId { get; set; }
    public Skill_JDDTO Skill_JDDTO { get; set; }

    public CreateSkill_JDCommand(Guid SkillId, Guid JobDescriptionId, Skill_JDDTO skill_EmployeeDTO)
    {
        JobDescriptionId = JobDescriptionId;
        SkillId = SkillId;
        Skill_JDDTO = skill_EmployeeDTO;
    }
}

public class CreateSkill_JDCommandHandler : IRequestHandler<CreateSkill_JD, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateSkill_JDCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(CreateSkill_JD request, CancellationToken cancellationToken)
    {
        var employee = await _context.Skill_JDs
            .Where(e => e.Id == request.SkillId && )
            .FirstOrDefaultAsync();

        if (employee != null && request.Skill_EmployeeDTO.Skill != null)
        {
            var skill = new Skill
            {
                Id = Guid.NewGuid(),
                SkillName = request.Skill_EmployeeDTO.Skill.SkillName,
                Skill_Description = request.Skill_EmployeeDTO.Skill.Skill_Description
            };

            var empSkill = new Skill_Employee
            {
                Id = new Guid(),
                SkillId = skill.Id,
                EmployeeId = employee.Id,
                Level = request.Skill_EmployeeDTO.Level
            };

            await _context.Skill_Employees.AddAsync(empSkill);
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync(cancellationToken);
            return "ádsa";
        }

        return "ádsa";
    }
}
*/