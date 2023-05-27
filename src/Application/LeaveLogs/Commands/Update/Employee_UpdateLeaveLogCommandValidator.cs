using FluentValidation;

namespace hrOT.Application.LeaveLogs.Commands.Update
{
    public class Employee_UpdateLeaveLogCommandValidator : AbstractValidator<Employee_UpdateLeaveLogCommand>
    {
        public Employee_UpdateLeaveLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Leave log ID is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.");

            RuleFor(x => x.LeaveHours)
                .NotEmpty().WithMessage("Leave hours are required.")
                .GreaterThanOrEqualTo(0).WithMessage("Leave hours must be greater than or equal to 0.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Reason is required.")
                .MaximumLength(200).WithMessage("Reason must not exceed 200 characters.");
        }
    }
}
