using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public string SkillName { get; set; }
    public Skills_EmployeeCommandDTO Skill_EmployeeDTO { get; set; }

    public Employee_CreateSkillCommand(Guid EmployeeID, string skillName, Skills_EmployeeCommandDTO skill_EmployeeDTO)
    {
        EmployeeId = EmployeeID;
        SkillName = skillName;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }
}

public class Employee_CreateSkillCommandHandler : IRequestHandler<Employee_CreateSkillCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefaultAsync();

        if (employee != null)
        {
            var skill = _context.Skills
                .Where(s => s.SkillName == request.SkillName)
                .FirstOrDefault();

            if (skill != null)
            {
                var empSkill = new Skill_Employee
                {
                    Id = new Guid(),
                    SkillId = skill.Id,
                    EmployeeId = employee.Id,
                    Level = request.Skill_EmployeeDTO.Level
                };
                await _context.Skill_Employees.AddAsync(empSkill);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        return false;
    }
}