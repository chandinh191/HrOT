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
            .NotEmpty().WithMessage("NameProject is required.")
            .MaximumLength(200).WithMessage("NameProject must not exceed 200 characters.")
            .MustAsync(BeUniqueProjectName).WithMessage("The specified name already exist.");

        RuleFor(v => v.Experience.TeamSize)
            .GreaterThan(0).WithMessage("TeamSize must be greater than 0.")
            .LessThanOrEqualTo(10).WithMessage("Team must be less than or equal to 10.");

        RuleFor(v => v.Experience.StartDate)
            .NotNull().WithMessage("StartDate is required.")
            .LessThan(v => v.Experience.EndDate).WithMessage("Start date must be before End date.");

        RuleFor(v => v.Experience.EndDate)
            .NotNull().WithMessage("EndDate is required.")
            .GreaterThan(d => d.Experience.StartDate).WithMessage("End date must be greater than Start date.");

        RuleFor(v => v.Experience.Description)
            .NotEmpty().WithMessage("Description is required.");

        RuleFor(v => v.Experience.TechStack)
            .NotEmpty().WithMessage("TechStack is required.");

        RuleFor(v => v.Experience.Status)
            .NotNull().WithMessage("Status is required.");
    }

    private async Task<bool> BeUniqueProjectName(Employee_ExperienceUpdateCommand experienceUpdateCommand
        , string arg1, CancellationToken arg2)
    {
        return await _context.Experiences
            .Where(e => e.Id == experienceUpdateCommand.Experience.Id)
            .AllAsync(n => n.NameProject != arg1, arg2);
    }
}