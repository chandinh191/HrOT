using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Commands.CreateTodoList;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Commands.CreatePosition;

public class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePositionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Tên không được để trống")
            .MaximumLength(200).WithMessage("Tên không được vượt quá 200 chữ");
           
    }

    
}
