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
        RuleFor(v => v.SkillName)
            .NotEmpty().WithMessage("Tên kĩ năng không được để trống.")
            .MustAsync(BeUniqueName).WithMessage("Nhân viên đã tồn tại kĩ năng này.");
        RuleFor(v => v.Skill_EmployeeDTO.Level)
            .NotEmpty().WithMessage("Cấp bậc không được để trống.");
    }

    private async Task<bool> BeUniqueName(Employee_CreateSkillCommand employee_CreateSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skill_Employees
            .Where(s => s.Skill.SkillName == employee_CreateSkillCommand.SkillName && s.IsDeleted == false)
            .AllAsync(s => s.Skill.SkillName != arg1, arg2);
    }
}