using FluentValidation;
using hrOT.Domain.Enums;

namespace hrOT.Application.LeaveLogs.Commands.Update
{
    public class Staff_UpdateLeaveLogCommandValidator : AbstractValidator<Staff_UpdateLeaveLogCommand>
    {
        public Staff_UpdateLeaveLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Leave log ID is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid leave log status.");
        }
    }
}
