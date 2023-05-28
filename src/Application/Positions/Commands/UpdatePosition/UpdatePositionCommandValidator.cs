using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Positions.Commands.CreatePosition;

namespace hrOT.Application.Positions.Commands.UpdatePosition;

public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

    }


}
