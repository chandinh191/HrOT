using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Auth.Queries;
public class LoginValidator : AbstractValidator<Login>
{
    private readonly IApplicationDbContext _context;
    public LoginValidator(IApplicationDbContext context)
    {
        _context = context;


        RuleFor(e => e.Username)

            .NotEmpty().WithMessage("Tài khoản không được để trống")
            .MaximumLength(50).WithMessage("Tên người dùng không được vượt quá 50 ký tự.");
          



        RuleFor(e => e.Password)

            .NotEmpty().WithMessage("Mật khẩu không được để trống")
            .MinimumLength(6).WithMessage("Mật khẩu phải có ít nhất 6 ký tự.");


    }

    public async Task<bool> BeUniqueUserName(string username, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .AllAsync(e => e.ApplicationUser.UserName != username, cancellationToken);
    }

    

   

   

}
