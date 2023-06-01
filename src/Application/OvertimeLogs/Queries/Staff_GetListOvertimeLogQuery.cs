using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.OvertimeLogs.Queries;
using hrOT.Domain.Enums;
using MediatR;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Security;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.OvertimeLogs.Queries;

public record Staff_GetListOvertimeLogQuery : IRequest<List<OvertimeLogDto>>;

public class Staff_GetListOvertimeLogQueryHandler : IRequestHandler<Staff_GetListOvertimeLogQuery, List<OvertimeLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Staff_GetListOvertimeLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<OvertimeLogDto>> Handle(Staff_GetListOvertimeLogQuery request, CancellationToken cancellationToken)
    {
        return await _context.OvertimeLogs
                .AsNoTracking()
                .Where(o => o.IsDeleted == false)
                .ProjectTo<OvertimeLogDto>(_mapper.ConfigurationProvider)
                .OrderBy(o => o.Status)
                .ToListAsync(cancellationToken);
    }
}
