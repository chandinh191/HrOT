using FluentValidation;
using hrOT.Domain.Enums;

namespace hrOT.Application.OvertimeLogs.Commands.Create
{
    public class Employee_CreateOvertimeLogCommandValidator : AbstractValidator<Employee_CreateOvertimeLogCommand>
    {
        public Employee_CreateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("ID nhân viên không được để trống");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Ngày bắt đầu phải sớm hơn hoặc bằng ngày kết thúc.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.");
            RuleFor(x => x.TotalHours)
                .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
                .GreaterThan(0).WithMessage("Tổng giờ phải lớn hơn 0.");
        }
    }
}
