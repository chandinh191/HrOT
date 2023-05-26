using FluentValidation;
using hrOT.Domain.Enums;
using hrOT.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Commands.Update
{
    public class Staff_UpdateOvertimeLogCommandValidator : AbstractValidator<Staff_UpdateOvertimeLogCommand>
    {
        public Staff_UpdateOvertimeLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Overtime log ID is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid overtime log status.");

        }
    }
}
