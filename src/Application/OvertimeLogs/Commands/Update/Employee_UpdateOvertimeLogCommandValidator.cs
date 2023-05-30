using FluentValidation;

namespace hrOT.Application.OvertimeLogs.Commands.Update
{
    public class Employee_UpdateOvertimeLogCommandValidator : AbstractValidator<Employee_UpdateOvertimeLogCommand>
    {
        public Employee_UpdateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.Id)
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
