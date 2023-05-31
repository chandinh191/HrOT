using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.PaySlips.Queries;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Commands.CreatePaySlip;
public class CreatePaySlipCommandValidator : AbstractValidator<CreatePaySlipCommand>
{
    private readonly IApplicationDbContext _context;
    public CreatePaySlipCommandValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(query => query.EmployeeId)
            .NotEmpty().WithMessage("Id nhân viên không được bỏ trống")
            .MustAsync(ExistAsync).WithMessage("Id nhân viên không tồn tại");
    }

    private async Task<bool> ExistAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellationToken);
        return employeeExists;
    }
}
