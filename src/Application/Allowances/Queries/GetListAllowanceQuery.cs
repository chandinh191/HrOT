using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Security;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Allowances.Queries;

public record GetListAllowanceQuery : IRequest<AllowanceList>;

public class GetListAllowanceQueryHandler : IRequestHandler<GetListAllowanceQuery, AllowanceList>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListAllowanceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AllowanceList> Handle(GetListAllowanceQuery request, CancellationToken cancellationToken)
    {
        return new AllowanceList
        {
            Lists = await _context.Allowances
                .AsNoTracking()
                .ProjectTo<AllowanceDto>(_mapper.ConfigurationProvider)
                .Where(o => o.IsDeleted == false)
                .ToListAsync(cancellationToken)
        };
    }
}
