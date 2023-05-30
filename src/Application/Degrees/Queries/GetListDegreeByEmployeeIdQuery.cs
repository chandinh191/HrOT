using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.CompanyContracts.Queries;
using hrOT.Domain.Enums;
using MediatR;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Degrees.Queries;

public record GetListDegreeByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<DegreeDto>>;
public class GetListDegreeByEmployeeIdQueryHandler : IRequestHandler<GetListDegreeByEmployeeIdQuery, List<DegreeDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListDegreeByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DegreeDto>> Handle(GetListDegreeByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
     
        return await _context.Degrees
            .Where(x => x.EmployeeId == request.EmployeeId)
            .ProjectTo<DegreeDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
