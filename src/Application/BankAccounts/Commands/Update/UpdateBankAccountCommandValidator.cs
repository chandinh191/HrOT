using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.BankAccounts.Commands.Update;
public class UpdateBankAccountCommandValidator : AbstractValidator<UpdateBankAccountCommand>
{
    public UpdateBankAccountCommandValidator()
    {
        RuleFor(x => x._dto.BankAccountNumber)
            .NotEmpty().WithMessage("Số tài khoản không được bỏ trống.")
            .MaximumLength(14).WithMessage("Số tài khoản không được quá 14 ký tự.");

        RuleFor(x => x._dto.BankAccountName)
            .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
            .MaximumLength(100).WithMessage("Tên tài khoản không được quá 100 ký tự.");
    }
}
