using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;


namespace hrOT.Application.Positions.Commands.CreatePosition;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;
       
    
        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Không được bỏ trống tên vị trí.")
            .MaximumLength(100).WithMessage("Vị trí không được vượt quá 100 ký tự.");
           
    }

    
}
