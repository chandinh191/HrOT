﻿
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.UpdatePosition;

public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(n => n.Id)
             .NotEmpty().WithMessage("Id không được bỏ trống")
             .MustAsync(ExistAsync).WithMessage("Id không tồn tại");
        RuleFor(n => n.Name)
           .NotEmpty().WithMessage("Không được bỏ trống tên vị trí.")
           .MaximumLength(100).WithMessage("Vị trí không được vượt quá 100 ký tự.");

    }
    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var Exists = await _context.Positions.AnyAsync(e => e.Id == Id, cancellationToken);
        return Exists;
    }

}
