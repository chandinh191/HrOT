using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.EmployeeSkill.Commands.Add;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Add;

public class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateSkillCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        //RuleFor(v => v.SkillDTO.Level)
        //   .NotEmpty().WithMessage("Cấp bậc không được để trống.");
        RuleFor(v => v.SkillDTO.SkillName)
            .NotEmpty().WithMessage("Tên kĩ năng không được để trống.")
            .MustAsync(BeUniqueName).WithMessage("Tên kĩ năng đã tồn tại.");

        RuleFor(v => v.SkillDTO.Skill_Description)
            .NotEmpty().WithMessage("Mô tả kĩ năng không được để trống.");
    }

    private async Task<bool> BeUniqueName(CreateSkillCommand createSkillCommand, string arg1, CancellationToken arg2)
    {
        return await _context.Skills
            .Where(s => s.SkillName == createSkillCommand.SkillDTO.SkillName && s.IsDeleted == false)
            .AllAsync(s => s.SkillName != arg1, arg2);
    }
}