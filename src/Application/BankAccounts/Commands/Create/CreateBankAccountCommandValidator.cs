using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.BankAccounts.Commands.Create;
public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
{
    public CreateBankAccountCommandValidator()
    {
        RuleFor(x => x.BankId)
            .NotEmpty().WithMessage("Bank ID không được bỏ trống.");

        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("Employee ID không được bỏ trống.");

        RuleFor(x => x.BankAccountDTO.BankAccountNumber)
            .NotEmpty().WithMessage("Số tài khoản không được bỏ trống.")
            .MaximumLength(14).WithMessage("Số tài khoản không được quá 14 ký tự.");

        RuleFor(x => x.BankAccountDTO.BankAccountName)
            .NotEmpty().WithMessage("Tên tài khoản không được bỏ trống.")
            .MaximumLength(100).WithMessage("Tên tài khoản không được quá 100 ký tự.");
    }
}
