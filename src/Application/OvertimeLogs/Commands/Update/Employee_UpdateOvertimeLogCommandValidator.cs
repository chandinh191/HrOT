using FluentValidation;

namespace hrOT.Application.OvertimeLogs.Commands.Update
{
    public class Employee_UpdateOvertimeLogCommandValidator : AbstractValidator<Employee_UpdateOvertimeLogCommand>
    {
        public Employee_UpdateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID is required.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("End date is required.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("End date must be greater than or equal to the start date.");
        }
    }
}
