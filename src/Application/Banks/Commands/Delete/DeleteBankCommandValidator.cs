using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Banks.Commands.Delete;
public class DeleteBankCommandValidator : AbstractValidator<DeleteBankCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteBankCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Id không được bỏ trống")
            .MustAsync(ExistAsync).WithMessage("Id không tồn tại");
    }

    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var bankExists = await _context.Banks.AnyAsync(e => e.Id == Id, cancellationToken);
        return bankExists;
    }
}
