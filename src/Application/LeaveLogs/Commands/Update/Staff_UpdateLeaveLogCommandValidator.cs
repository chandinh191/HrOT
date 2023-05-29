using FluentValidation;
using hrOT.Domain.Enums;

namespace hrOT.Application.LeaveLogs.Commands.Update
{
    public class Staff_UpdateLeaveLogCommandValidator : AbstractValidator<Staff_UpdateLeaveLogCommand>
    {
        public Staff_UpdateLeaveLogCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID của Leave Log không được để trống");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái không hợp lệ.");
        }
    }
}
