using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.CreatePosition;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(n => n.DepartmentId )
             .NotEmpty().WithMessage("Id phòng ban không được bỏ trống")
             .MustAsync(ExistAsync).WithMessage("Id phòng ban không tồn tại");

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Không được bỏ trống tên vị trí.")
            .MaximumLength(100).WithMessage("Vị trí không được vượt quá 100 ký tự.");
           
    }
    private async Task<bool> ExistAsync(Guid DepartmentId, CancellationToken cancellationToken)
    {
        var Exists = await _context.Departments.AnyAsync(e => e.Id == DepartmentId, cancellationToken);
        return Exists;
    }
}
