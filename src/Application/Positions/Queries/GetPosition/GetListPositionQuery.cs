using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Levels;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Positions.Queries.GetPosition;

public record GetListPositionQuery : IRequest<List<PositionDTO>>;

public class GetListPositionQueryHandler : IRequestHandler<GetListPositionQuery, List<PositionDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListPositionQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PositionDTO>> Handle(GetListPositionQuery request, CancellationToken cancellationToken)
    {
        return await _context.Positions
               .ProjectTo<PositionDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
    }
}


