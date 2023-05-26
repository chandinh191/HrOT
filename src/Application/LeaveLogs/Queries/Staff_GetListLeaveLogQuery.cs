using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.LeaveLogs.Queries;
using hrOT.Domain.Enums;
using MediatR;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Security;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.LeaveLogs.Queries;

public record Staff_GetListLeaveLogQuery : IRequest<LeaveLogList>;

public class Staff_GetListLeaveLogQueryHandler : IRequestHandler<Staff_GetListLeaveLogQuery, LeaveLogList>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Staff_GetListLeaveLogQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LeaveLogList> Handle(Staff_GetListLeaveLogQuery request, CancellationToken cancellationToken)
    {
        return new LeaveLogList
        {
            Lists = await _context.LeaveLogs
                .AsNoTracking()
                .ProjectTo<LeaveLogDto>(_mapper.ConfigurationProvider)
                .Where(o => o.IsDeleted == false)
                .OrderBy(t => t.Status)
                .ToListAsync(cancellationToken)
        };
    }
}
