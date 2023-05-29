using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.JobDescriptions.Commands.CreateJobDescription;

namespace hrOT.Application.JobDescriptions.Commands.UpdateJobDescription;

public class UpdateJonDescriptionCommandValidator : AbstractValidator<UpdateJobDescriptionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateJonDescriptionCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Tiêu đề không được bỏ trống.")
            .MaximumLength(100).WithMessage("Tiêu đề không được vượt quá 100 ký tự..");
        RuleFor(Dt => Dt.Description)
            .NotEmpty().WithMessage("Mô tả không được bỏ trống.")
            .MaximumLength(200).WithMessage("Mô tả không được vượt quá 200 ký tự..");
        RuleFor(St => St.StartDate)
            .NotEmpty().WithMessage("Ngày bắt đầu không được bỏ trống.")
            .Must(BeValidStartDate).WithMessage("Ngày bắt đầu phải lớn hơn hoặc bằng ngày hiện tại.");
        RuleFor(Et => Et.EndDate)
            .NotEmpty().WithMessage("Ngày kết thúc không được bỏ trống.")
            .GreaterThan(et => et.StartDate).WithMessage("Ngày kết thúc phải lớn hơn ngày bắt đầu.");
        RuleFor(SAt => SAt.Status)
            .NotEmpty().WithMessage("Status không được bỏ trống.")
            .MaximumLength(100).WithMessage("Status không được vượt quá 100 ký tự.");
    }
    private bool BeValidStartDate(DateTime startDate)
    {
        return startDate.Date >= DateTime.Now.Date;
    }
}


