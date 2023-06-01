using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.PaySlips.Commands.CreatePaySlip;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Degrees.Commands.Delete;

public class DeleteDegreeCommandValidator : AbstractValidator<DeleteDegreeCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteDegreeCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Id không được bỏ trống")
            .MustAsync(ExistAsync).WithMessage("Id không tồn tại");
    }

    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Degrees.AnyAsync(e => e.Id == Id, cancellationToken);
        return employeeExists;
    }
}