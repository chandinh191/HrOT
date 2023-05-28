using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skills.Queries;
public class GetSkillListQuery : IRequest<List<SkillDTO>>
{
}

public class GetSkillListQueryHandler : IRequestHandler<GetSkillListQuery, List<SkillDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetSkillListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<SkillDTO>> Handle(GetSkillListQuery request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.IsDeleted == false)
            .ProjectTo<SkillDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return skill;
    }
}
