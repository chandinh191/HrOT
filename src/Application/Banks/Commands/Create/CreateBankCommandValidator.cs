using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace hrOT.Application.Banks.Commands.Create;
public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
{
    public CreateBankCommandValidator()
    {
        RuleFor(x => x.BankName)
            .NotEmpty().WithMessage("Tên ngân hàng không được bỏ trống.")
            .MaximumLength(100).WithMessage("Tên ngân hàng không được quá 100 ký tự.");
    }
}
