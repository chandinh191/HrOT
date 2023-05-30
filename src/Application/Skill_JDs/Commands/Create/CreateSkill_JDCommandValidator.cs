using FluentValidation;

namespace hrOT.Application.Skill_JDs.Commands.Create
{
    public class CreateSkill_JDCommandValidator : AbstractValidator<CreateSkill_JDCommand>
    {
        public CreateSkill_JDCommandValidator()
        {
            RuleFor(x => x.SkillId)
                .NotEmpty().WithMessage("Skill ID không được để trống.");

            RuleFor(x => x.JobDescriptionId)
                .NotEmpty().WithMessage("Job Description ID không được để trống.");

            RuleFor(x => x.Level)
                .NotEmpty().WithMessage("Level không được để trống.")
                .MaximumLength(100).WithMessage("Level không được quá 100 ký tự.");
        }
    }
}
