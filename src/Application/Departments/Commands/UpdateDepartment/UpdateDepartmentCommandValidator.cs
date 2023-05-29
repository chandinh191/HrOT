
using FluentValidation;
using hrOT.Application.Common.Interfaces;


namespace hrOT.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateDepartmentCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Không được bỏ trống tên phòng ban.")
            .MaximumLength(100).WithMessage("Tên phòng ban không được vượt quá 100 ký tự.");
        RuleFor(d => d.Description)
            .NotEmpty().WithMessage("Không được bỏ trống mô tả.")
            .MaximumLength(200).WithMessage("Mô tả không được vượt quá 200 ký tự.");

    }


}

