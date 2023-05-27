using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees_Skill;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid SkillId { get; set; }

    public Skill_EmployeeDTO Skill_EmployeeDTO { get; set; }

    public Employee_UpdateSkillCommand(Guid EmployeeID, Guid SkillID, Skill_EmployeeDTO skill_EmployeeDTO)
    {
        EmployeeId = EmployeeID;
        SkillId = SkillID;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }
}

public class Employee_UpdateSkillCommandHandler : IRequestHandler<Employee_UpdateSkillCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_UpdateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var empskill = await _context.Skill_Employees
            .Include(s => s.Skill)
            .Where(e => e.EmployeeId == request.EmployeeId && e.SkillId == request.SkillId)
            .FirstOrDefaultAsync();

        if (empskill != null && request.Skill_EmployeeDTO.Skill != null)
        {
            empskill.Level = request.Skill_EmployeeDTO.Level;
            empskill.Skill.SkillName = request.Skill_EmployeeDTO.Skill.SkillName;
            empskill.Skill.Skill_Description = request.Skill_EmployeeDTO.Skill.Skill_Description;

            _context.Skill_Employees.Update(empskill);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}