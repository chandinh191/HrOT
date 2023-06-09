
using FluentValidation;
using hrOT.Application.Common.Interfaces;


namespace hrOT.Application.Holidays.Commands.CreateHoliday;

public class CreateHolidayCommandValidator : AbstractValidator<CreateHolidayCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateHolidayCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(dn => dn.DateName)
            .NotEmpty().WithMessage("Không được bỏ trống tên ngày nghỉ.")
            .MaximumLength(100).WithMessage("Tên phòng ban không được vượt quá 100 ký tự.");
        RuleFor(d => d.Day)
            .NotEmpty().WithMessage("Không được bỏ trống ngày nghỉ.")
            .GreaterThanOrEqualTo(DateTime.Today).WithMessage("Ngày nghỉ phải lớn hơn hoặc bằng ngày hiện tại.");
        RuleFor(h => h.HourlyPay)
           .NotEmpty().WithMessage("Không được bỏ trống lương theo giờ.")
           .GreaterThanOrEqualTo(0).WithMessage("Lương theo giờ không được âm.");

    }


}
