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
        RuleFor(v => v.SkillId)
            .NotEmpty().WithMessage("Id kĩ năng không được để trống.");
            //.MustAsync(BeUniqueName).WithMessage("Nhân viên đã tồn tại kĩ năng này.");
        RuleFor(v => v.Skill_EmployeeDTO.Level)
            .NotEmpty().WithMessage("Cấp bậc không được để trống.");
    }

    private async Task<bool> BeUniqueName(Employee_CreateSkillCommand employee_CreateSkillCommand, Guid arg1, CancellationToken arg2)
    {
        var result = await _context.Skill_Employees
            .Where(s => s.Skill.Id == employee_CreateSkillCommand.SkillId && s.IsDeleted == false)
            .AllAsync(s => s.Skill.Id != arg1, arg2);
        return result;
    }
}