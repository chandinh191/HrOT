using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Positions.Commands.UpdatePosition;

namespace hrOT.Application.Levels.Commands.CreateLevel;

public class CreateLevelCommandValidator : AbstractValidator<CreateLevelCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateLevelCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(200).WithMessage("Description must not exceed 200 characters.");

    }


}

