using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Queries;

public record GetAllFamilyQuery : IRequest<List<FamilyDto>>;

public class GetAllDegreeQueryHandler : IRequestHandler<GetAllFamilyQuery, List<FamilyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllDegreeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FamilyDto>> Handle(GetAllFamilyQuery request, CancellationToken cancellationToken)
    {

        var list = await _context.Families
            .Where(e => e.IsDeleted == false)
            .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}
