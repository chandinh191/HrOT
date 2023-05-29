using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.InterviewProcesses.Commands.Create;
public class CreateInterviewProcessCommandValidator : AbstractValidator<CreateInterviewProcessCommand>
{
    private readonly IApplicationDbContext _context;
    public CreateInterviewProcessCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(command => command.EmployeeId)
            .NotEmpty().WithMessage("ID của nhân viên không được để trống.")
            .MustAsync(BeExistingEmployeeId).WithMessage("EmployeeId không tồn tại.");

        RuleFor(command => command.JobDescriptionId)
            .NotEmpty().WithMessage("ID của mô tả công việc không được để trống.")
            .MustAsync(BeExistingJobDescriptionId).WithMessage("ID của mô tả công việc không tồn tại.");

        RuleFor(command => command.DayTime)
            .NotEmpty().WithMessage("Ngày phỏng vấn không được để trống.");

        RuleFor(command => command.Place)
            .NotEmpty().WithMessage("Địa điểm không được để trống.");

        RuleFor(command => command.FeedBack)
            .NotEmpty().WithMessage("FeedBack không được để trống.");
    }

    private async Task<bool> BeExistingEmployeeId(Guid employeeId, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AnyAsync(l => l.Id == employeeId, cancellationToken);
    }

    private async Task<bool> BeExistingJobDescriptionId(Guid jobDescriptionId, CancellationToken cancellationToken)
    {
        return await _context.JobDescriptions
            .AnyAsync(l => l.Id == jobDescriptionId, cancellationToken);
    }
}
