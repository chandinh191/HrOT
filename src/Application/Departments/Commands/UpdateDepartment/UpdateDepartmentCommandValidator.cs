using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Departments.Commands.CreateDepartment;

namespace hrOT.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Tên không được để trống.")
            .MaximumLength(200).WithMessage("Tên không được vượt quá 200 chữ");
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Mô tả không được để trống.")
            .MaximumLength(200).WithMessage("Mô tả không được vượt quá 200 chữ");

    }


}

