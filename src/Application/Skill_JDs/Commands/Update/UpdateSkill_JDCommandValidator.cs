using FluentValidation;

namespace hrOT.Application.Skill_JDs.Commands.Update
{
    public class UpdateSkill_JDCommandValidator : AbstractValidator<UpdateSkill_JDCommand>
    {
        public UpdateSkill_JDCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Skill JD ID không được để trống.");

            RuleFor(x => x.SkillId)
                .NotEmpty().WithMessage("Skill ID không được để trống.");

            RuleFor(x => x.JobDescriptionId)
                .NotEmpty().WithMessage("Job Description ID không được để trống.");

            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Level không được để trống.")
                .MaximumLength(100).WithMessage("Level không được lớn hơn 100 ký tự");
        }
    }
}
