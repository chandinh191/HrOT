using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Positions.Commands.UpdatePosition;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.DeletePosition;
public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public DeletePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(n => n.Id)
             .NotEmpty().WithMessage("Id không được bỏ trống")
             .MustAsync(ExistAsync).WithMessage("Id không tồn tại");

    }
    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var Exists = await _context.Positions.AnyAsync(e => e.Id == Id, cancellationToken);
        return Exists;
    }

}