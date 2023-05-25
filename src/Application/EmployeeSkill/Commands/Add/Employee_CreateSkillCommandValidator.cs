using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommandValidator : AbstractValidator<Employee_CreateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_CreateSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Skill_EmployeeDTO.Level)
            .NotEmpty().WithMessage("Level is required.");
        RuleFor(v => v.Skill_EmployeeDTO.Skill.SkillName)
            .NotEmpty().WithMessage("SkillName is required.")
            .MustAsync(BeUniqueName).WithMessage("Skill name already exist.");

        RuleFor(v => v.Skill_EmployeeDTO.Skill.Skill_Description)
            .NotEmpty().WithMessage("Skill_Description is required.");
    }

    private async Task<bool> BeUniqueName(Employee_CreateSkillCommand createSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skill_Employees
            .Where(s => s.SkillId == createSkillCommand.Skill_EmployeeDTO.Skill.ID)
            .AllAsync(s => s.Skill.SkillName != arg1, arg2);
    }
}