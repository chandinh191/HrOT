using FluentValidation;
using hrOT.Domain.Enums;

namespace hrOT.Application.LeaveLogs.Commands.Update
{
    public class Staff_UpdateLeaveLogCommandValidator : AbstractValidator<Staff_UpdateLeaveLogCommand>
    {
        public Staff_UpdateLeaveLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Leave log ID is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Start date must be less than or equal to the end date.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to the start date.");

            RuleFor(x => x.LeaveHours)
                .NotEmpty().WithMessage("Leave hours are required.")
                .GreaterThanOrEqualTo(0).WithMessage("Leave hours must be greater than or equal to 0.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(200).WithMessage("Reason must not exceed 200 characters.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid leave log status.");
        }
    }
}
