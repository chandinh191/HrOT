using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;

namespace hrOT.Application.Skills.Commands.Add;

public class CreateSkillCommand : IRequest<bool>
{
    public SkillCommandDTO SkillDTO { get; set; }

    public CreateSkillCommand(SkillCommandDTO skillDTO)
    {
        SkillDTO = skillDTO;
    }
}

public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = new Skill
        {
            Id = new Guid(),
            SkillName = request.SkillDTO.SkillName,
            Skill_Description = request.SkillDTO.Skill_Description
        };

        await _context.Skills.AddAsync(skill);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}