using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using LogOT.Application.Employees;
using MediatR;
using AutoMapper.QueryableExtensions;

using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Degrees.Queries;

public record GetAllDegreeQuery : IRequest<List<DegreeDto>>;

public class GetAllDegreeQueryHandler : IRequestHandler<GetAllDegreeQuery, List<DegreeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllDegreeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DegreeDto>> Handle(GetAllDegreeQuery request, CancellationToken cancellationToken)
    {

        var list = await _context.Degrees
            .Where(e => e.IsDeleted == false)
            .ProjectTo<DegreeDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}
