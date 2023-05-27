using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using LogOT.Application.Employees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Skill_JDs.Queries;
public record GetAllSkill_JD : IRequest<List<Skill_JDDTO>>;

public class GetAllSkill_JDHandler : IRequestHandler<GetAllSkill_JD, List<Skill_JDDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllSkill_JDHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Skill_JDDTO>> Handle(GetAllSkill_JD request, CancellationToken cancellationToken)
    {

        var list = await _context.Skill_JDs
            .Where(e => e.IsDeleted == false)
            .ProjectTo<Skill_JDDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}
