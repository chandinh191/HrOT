using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Families;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Families.Queries;

public record GetListFamilyByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<FamilyDto>>;
public class GetListFamilyByEmployeeIdQueryHandler : IRequestHandler<GetListFamilyByEmployeeIdQuery, List<FamilyDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListFamilyByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<FamilyDto>> Handle(GetListFamilyByEmployeeIdQuery request, CancellationToken cancellationToken)
    {

        return await _context.Families
            .Where(x => x.EmployeeId == request.EmployeeId)
            .ProjectTo<FamilyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
