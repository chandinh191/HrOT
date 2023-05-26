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
            .NotEmpty().WithMessage("Cấp bậc không được để trống.");
        RuleFor(v => v.Skill_EmployeeDTO.Skill.SkillName)
            .NotEmpty().WithMessage("Tên kĩ năng không được để trống.")
            .MustAsync(BeUniqueName).WithMessage("Tên kĩ năng đã tồn tại.");

        RuleFor(v => v.Skill_EmployeeDTO.Skill.Skill_Description)
            .NotEmpty().WithMessage("Mô tả kĩ năng không được để trống.");
    }

    private async Task<bool> BeUniqueName(Employee_CreateSkillCommand createSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skill_Employees
            .Where(s => s.SkillId == createSkillCommand.Skill_EmployeeDTO.Skill.ID &&
                                    s.EmployeeId == createSkillCommand.EmployeeId)
            .AllAsync(s => s.Skill.SkillName != arg1, arg2);
    }
}