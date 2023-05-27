using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommandValidator : AbstractValidator<Employee_UpdateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateSkillCommandValidator(IApplicationDbContext context)
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

    private async Task<bool> BeUniqueName(Employee_UpdateSkillCommand updateSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skill_Employees
            .Where(s => s.SkillId == updateSkillCommand.SkillId &&
                                    s.EmployeeId == updateSkillCommand.EmployeeId)
            .AllAsync(s => s.Skill.SkillName != arg1, arg2);
    }
}