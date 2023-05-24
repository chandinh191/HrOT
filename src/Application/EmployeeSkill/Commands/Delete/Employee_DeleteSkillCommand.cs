using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Delete;

public class Employee_DeleteSkillCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Guid SkillId { get; set; }

    public Employee_DeleteSkillCommand(Guid EmployeeID, Guid SkillID)
    {
        EmployeeId = EmployeeID;
        SkillId = SkillID;
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
        var empskill = await _context.Skill_Employees
            .Where(e => e.EmployeeId == request.EmployeeId && e.SkillId == request.SkillId)
            .FirstOrDefaultAsync();
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();

        if (empskill != null && skill != null)
        {
            empskill.IsDeleted = true;
            skill.IsDeleted = true;

            _context.Skill_Employees.Update(empskill);
            _context.Skills.Update(skill); 
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;
    }
}