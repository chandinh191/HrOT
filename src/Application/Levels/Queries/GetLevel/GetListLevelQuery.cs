using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Levels.Queries.GetLevel;
public record GetListLevelQuery : IRequest<List<LevelDTO>>;

public class GetListLevelQueryHandler : IRequestHandler<GetListLevelQuery, List<LevelDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListLevelQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LevelDTO>> Handle(GetListLevelQuery request, CancellationToken cancellationToken)
    {
        return await _context.Levels
                .ProjectTo<LevelDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}

