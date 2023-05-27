using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;

namespace hrOT.Application.Exchanges.Commands.CreateExchange;
public class CreateExchangeCommandValidator : AbstractValidator<CreateExchangeCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateExchangeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Muc_quy_doi)
            .NotNull().WithMessage("Mức quy đổi không được để trống.")
            .GreaterThan(0).WithMessage("Mức quy đổi phải lớn hơn 0.");

        RuleFor(v => v.Giam_tru)
            .NotNull().WithMessage("Giảm trừ không được để trống.");

        RuleFor(v => v.Thue_suat)
            .NotNull().WithMessage("Thuế suất không được để trống.")
            .GreaterThan(0).WithMessage("Thuế suất phải lớn hơn 0.");
    }
}

