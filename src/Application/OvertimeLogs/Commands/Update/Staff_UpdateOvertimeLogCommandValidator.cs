using FluentValidation;
using hrOT.Domain.Enums;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Update
{
    public class Staff_UpdateOvertimeLogCommandValidator : AbstractValidator<Staff_UpdateOvertimeLogCommand>
    {
        public Staff_UpdateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Overtime log ID is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start date must be less than or equal to the end date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to the start date.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid overtime log status.");

        }
    }
}
