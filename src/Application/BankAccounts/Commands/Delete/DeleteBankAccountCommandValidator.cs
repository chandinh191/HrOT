using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Commands.Delete;
public class DeleteBankAccountCommandValidator : AbstractValidator<DeleteBankAccountCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteBankAccountCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(query => query.Id)
            .NotEmpty().WithMessage("Id không được bỏ trống")
            .MustAsync(ExistAsync).WithMessage("Id không tồn tại");
    }

    private async Task<bool> ExistAsync(Guid Id, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.BankAccounts.AnyAsync(e => e.Id == Id, cancellationToken);
        return employeeExists;
    }
}
