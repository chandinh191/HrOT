using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees_Skill;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommand : IRequest<bool>
{
    public Guid EmployeeId { get; set; }
    public Skill_EmployeeDTO Skill_EmployeeDTO { get; set; }

    public Employee_CreateSkillCommand(Guid EmployeeID, Skill_EmployeeDTO skill_EmployeeDTO)
    {
        EmployeeId = EmployeeID;
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
                SkillId= skill.Id,
                EmployeeId = employee.Id,
                Level = request.Skill_EmployeeDTO.Level
            };

            await _context.Skill_Employees.AddAsync(empSkill);
            await _context.Skills.AddAsync(skill);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}