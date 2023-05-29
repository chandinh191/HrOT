using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Skill_JDs.Queries;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skill_JDs.Queries;

public record GetSkill_JDListQuery : IRequest<Skill_JDList>;

public class GetSkill_JDListQueryHandler : IRequestHandler<GetSkill_JDListQuery, Skill_JDList>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSkill_JDListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Skill_JDList> Handle(GetSkill_JDListQuery request, CancellationToken cancellationToken)
    {
        return new Skill_JDList
        {
            Lists = await _context.Skill_JDs
                .AsNoTracking()
                .ProjectTo<Skill_JDDto>(_mapper.ConfigurationProvider)
                .Where(o => o.IsDeleted == false)
                .ToListAsync(cancellationToken)
        };
    }
}