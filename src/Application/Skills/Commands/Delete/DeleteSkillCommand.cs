using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Commands.Delete;

public class DeleteSkillCommand : IRequest<string>
{
    public Guid SkillId { get; set; }

    public DeleteSkillCommand(Guid skillId)
    {
        SkillId = skillId;
    }
}

public class DeleteSkillCommandHandler : IRequestHandler<DeleteSkillCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public DeleteSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id kĩ năng không tồn tại"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        skill.IsDeleted = true;

        _context.Skills.Update(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";
    }
}