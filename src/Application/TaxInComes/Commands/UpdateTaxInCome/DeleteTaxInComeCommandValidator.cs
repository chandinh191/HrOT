using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TaxInComes.Commands.CreateTaxInCome;

namespace hrOT.Application.TaxInComes.Commands.UpdateTaxInCome;
public class UpdateTaxInComeCommandValidator : AbstractValidator<UpdateTaxInComeCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTaxInComeCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Muc_chiu_thue)
            .NotNull().WithMessage("Muc_quy_doi is required.")
            .GreaterThan(0).WithMessage("Muc_quy_doi must be greater than 0.");

        RuleFor(v => v.Thue_suat)
            .NotNull().WithMessage("Thue_suat is required.")
            .GreaterThan(0).WithMessage("Thue_suat must be greater than 0.");
    }
}
