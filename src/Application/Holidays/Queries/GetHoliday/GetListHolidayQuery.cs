using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Holidays.Queries.GetHoliday;

public record GetListHolidayQuery : IRequest<List<HolidayDTO>>;

public class GetListHolidayQueryHandler : IRequestHandler<GetListHolidayQuery, List<HolidayDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListHolidayQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<HolidayDTO>> Handle(GetListHolidayQuery request, CancellationToken cancellationToken)
    {
        return await _context.Holidays
                 .ProjectTo<HolidayDTO>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
    }
}
