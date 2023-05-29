using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Employees.Commands.Create;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Auth.Queries;
public class ResetPasswordValidator : AbstractValidator<ResetPassword>
{
    private readonly IApplicationDbContext _context;

    public ResetPasswordValidator(IApplicationDbContext context)
    {
        _context = context;



        RuleFor(e => e.Email)

            .NotEmpty().WithMessage("Email không được để trống.")
            .MaximumLength(100).WithMessage("Email không được vượt quá 100 ký tự.")
            .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");
         

       

    }

    
    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AllAsync(e => e.ApplicationUser.Email != email, cancellationToken);
    }

   

}
