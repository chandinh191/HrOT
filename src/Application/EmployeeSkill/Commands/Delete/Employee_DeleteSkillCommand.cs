using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Delete;

public class Employee_DeleteSkillCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public string SkillName { get; set; }

    public Employee_DeleteSkillCommand(Guid EmployeeID, string skillName)
    {
        EmployeeId = EmployeeID;
        SkillName = skillName;
    }
}

public class Employee_DeleteSkillCommandHandler : IRequestHandler<Employee_DeleteSkillCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_DeleteSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.SkillName == request.SkillName)
            .FirstOrDefaultAsync();

        var empskill = await _context.Skill_Employees
            .Include(s => s.Skill)
            .Where(e => e.EmployeeId == request.EmployeeId && e.Skill.SkillName == skill.SkillName)
            .FirstOrDefaultAsync();

        if (empskill != null)
        {
            empskill.IsDeleted = true;

            _context.Skill_Employees.Update(empskill);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}