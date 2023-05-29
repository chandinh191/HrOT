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
            .NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(200).WithMessage("Tên không được vượt quá 200 chữ.");
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống")
            .MaximumLength(200).WithMessage("Mô tả không được vượt quá 200 chữ");
    }
}