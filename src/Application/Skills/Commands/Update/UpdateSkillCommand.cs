using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Update;

public class UpdateSkillCommand : IRequest<string>
{
    public Guid SkillId { get; set; }
    public SkillCommandDTO SkillDTO { get; set; }

    public UpdateSkillCommand(Guid skillId, SkillCommandDTO skillDTO)
    {
        SkillId = skillId;
        SkillDTO = skillDTO;
    }
}

public class UpdateSkillCommandHandler : IRequestHandler<UpdateSkillCommand, string>
{
    private readonly IApplicationDbContext _context;

    public UpdateSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id kĩ năng không tồn tại"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        var tempSkillName = skill.SkillName;

        skill.SkillName = request.SkillDTO.SkillName;
        skill.Skill_Description = request.SkillDTO.Skill_Description;

        _context.Skills.Update(skill);
        await _context.SaveChangesAsync(cancellationToken);

        var existSkillNameCheck = await _context.Skills
            .Where(s => s.SkillName == request.SkillDTO.SkillName)
            .CountAsync();

        if (existSkillNameCheck > 1)
        {
            skill.SkillName = tempSkillName;
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync(cancellationToken);

            return "Tên kĩ năng đã tồn tại";
        }

        return "Cập nhật thành công";
    }
}