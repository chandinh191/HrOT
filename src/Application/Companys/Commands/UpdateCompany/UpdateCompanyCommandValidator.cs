using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Companys.Commands.CreateCompany;
using hrOT.Application.Departments.Commands.CreateDepartment;

namespace hrOT.Application.Companys.Commands.UpdateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    private readonly IApplicationDbContext _context;

    [Obsolete]
    public CreateCompanyCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Không được bỏ trống tên công ty.")
            .MaximumLength(200).WithMessage("Tên công ty không dược vượt quá 200 ký tự.");
        RuleFor(ad => ad.Address)
            .NotEmpty().WithMessage("Không được bỏ trống địa chỉ.")
            .MaximumLength(200).WithMessage("Địa chỉ không dược vượt quá 200 ký tự.");
        RuleFor(ac => ac.AccountEmail)
            .NotEmpty().WithMessage("Không được bỏ trống Email.")
            .MaximumLength(50).WithMessage("Email không dược vượt quá 50 ký tự.")
            .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Email không hợp lệ.");

        RuleFor(p => p.Phone)
            .NotEmpty().WithMessage("Không được bỏ trống số điện thoại.")
            .MaximumLength(11).WithMessage("Số điện thoại không dược vượt quá 11 ký tự.");
        RuleFor(hr => hr.HREmail)
            .NotEmpty().WithMessage("Không được bỏ trống HR Email.")
            .MaximumLength(50).WithMessage("HR Email không dược vượt quá 50 ký tự.")
            .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("Email không hợp lệ.");

    }

    private object EmailAddress(EmailValidationMode net4xRegex)
    {
        throw new NotImplementedException();
    }
}

