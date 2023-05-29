
using FluentValidation;
using hrOT.Application.Common.Interfaces;


namespace hrOT.Application.Positions.Commands.UpdatePosition;

public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
           .NotEmpty().WithMessage("Không được bỏ trống tên vị trí.")
           .MaximumLength(100).WithMessage("Vị trí không được vượt quá 100 ký tự.");

    }


}
