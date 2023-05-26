using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Experiences.Commands;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Update;

public class Employee_ExperienceUpdateCommandValidator : AbstractValidator<Employee_ExperienceUpdateCommand>
{
    private readonly IApplicationDbContext _context;

    public Employee_ExperienceUpdateCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Experience.NameProject)
            .NotEmpty().WithMessage("Tên dự án không được để trống.")
            .MaximumLength(200).WithMessage("Tên dự án không được quá 200 chữ.")
            .MustAsync(BeUniqueProjectName).WithMessage("Tên dự án đã tồn tại.");

        RuleFor(v => v.Experience.TeamSize)
            .GreaterThan(0).WithMessage("Số lượng thành viên trong nhóm không được để trống.")
            .LessThanOrEqualTo(10).WithMessage("Số lượng thành viên trong nhóm không được vượt quá 10.");

        RuleFor(v => v.Experience.StartDate)
            .NotNull().WithMessage("StartDate is required.")
            .LessThan(v => v.Experience.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn ngày kết thúc.");

        RuleFor(v => v.Experience.EndDate)
            .NotNull().WithMessage("EndDate is required.")
            .GreaterThan(d => d.Experience.StartDate).WithMessage("Ngày kết thúc phải trễ hơn ngày bắt đầu");

        RuleFor(v => v.Experience.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.");

        RuleFor(v => v.Experience.TechStack)
            .NotEmpty().WithMessage("TechStack không được để trống.");

        RuleFor(v => v.Experience.Status)
            .NotNull().WithMessage("Trạng thái không được để trống.");
    }

    private async Task<bool> BeUniqueProjectName(Employee_ExperienceUpdateCommand experienceUpdateCommand
        , string arg1, CancellationToken arg2)
    {
        return await _context.Experiences
            .Where(e => e.EmployeeId == experienceUpdateCommand.EmployeeID)
            .AllAsync(n => n.NameProject != arg1, arg2);
    }
}