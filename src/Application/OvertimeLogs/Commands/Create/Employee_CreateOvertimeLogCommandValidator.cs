using FluentValidation;
using hrOT.Domain.Enums;

namespace hrOT.Application.OvertimeLogs.Commands.Create
{
    public class Employee_CreateOvertimeLogCommandValidator : AbstractValidator<Employee_CreateOvertimeLogCommand>
    {
        public Employee_CreateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("Employee ID is required.");

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
