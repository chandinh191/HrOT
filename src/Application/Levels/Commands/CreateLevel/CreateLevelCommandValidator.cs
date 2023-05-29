
using FluentValidation;
using hrOT.Application.Common.Interfaces;


namespace hrOT.Application.Levels.Commands.CreateLevel;

public class CreateLevelCommandValidator : AbstractValidator<CreateLevelCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateLevelCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Không được bỏ trống cấp bậc.")
            .MaximumLength(50).WithMessage("Cấp bậc không được vượt quá 50 ký tự.");
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Không được bỏ trống mô tả.")
            .MaximumLength(200).WithMessage("Mô tả khuông được vượt quá 200 ký tự.");

    }


}

