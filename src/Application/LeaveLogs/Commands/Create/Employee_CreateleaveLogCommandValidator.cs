using FluentValidation;

namespace hrOT.Application.LeaveLogs.Commands.Create
{
    public class Employee_CreateLeaveLogCommandValidator : AbstractValidator<Employee_CreateLeaveLogCommand>
    {
        public Employee_CreateLeaveLogCommandValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("ID nhân viên không được để trống.");

            RuleFor(x => x.LeaveHours)
                .NotEmpty().WithMessage("Giờ rời đi không được để trống.")
                .GreaterThanOrEqualTo(0).WithMessage("Giờ rời đi phải lớn hơn hoặc bằng 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Ngày bắt đầu không được để trống.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("Ngày kết thúc không được để trống.")
                .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Ngày kết thúc phải sau hoặc bằng ngày bắt đầu.");

            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Lí do không được để trống.")
                .MaximumLength(200).WithMessage("Lí do không được vượt quá 200 chữ.");
        }
    }
}
