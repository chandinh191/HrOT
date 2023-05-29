using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.InterviewProcesses.Commands.Create;

namespace hrOT.Application.InterviewProcesses.Commands.Update;
public class UpdateInterviewProcessCommandValidator : AbstractValidator<UpdateInterviewProcessCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateInterviewProcessCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(command => command.DayTime)
            .NotEmpty().WithMessage("Ngày phỏng vấn không được để trống.");

        RuleFor(command => command.Place)
            .NotEmpty().WithMessage("Địa điểm không được để trống.");

        RuleFor(command => command.FeedBack)
            .NotEmpty().WithMessage("FeedBack không được để trống.");

        RuleFor(command => command.Result)
            .NotEmpty().WithMessage("Kết quả phỏng vấn không được để trống.");
    }
}

