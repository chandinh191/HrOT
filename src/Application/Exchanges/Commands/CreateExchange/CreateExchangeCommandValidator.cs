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
            .NotNull().WithMessage("Muc_quy_doi is required.")
            .GreaterThan(0).WithMessage("Muc_quy_doi must be greater than 0.");

        RuleFor(v => v.Giam_tru)
            .NotNull().WithMessage("Giam_tru is required.")
            .GreaterThan(0).WithMessage("Giam_tru must be greater than 0.");

        RuleFor(v => v.Thue_suat)
            .NotNull().WithMessage("Thue_suat is required.")
            .GreaterThan(0).WithMessage("Thue_suat must be greater than 0.");
    }
}

