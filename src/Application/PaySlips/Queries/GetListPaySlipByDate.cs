using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Queries;
public record GetListPaySlipByDate(DateTime fromDate, DateTime toDate) : IRequest<List<PaySlipDto>>;
public class GetListPaySlipByDateHandler : IRequestHandler<GetListPaySlipByDate, List<PaySlipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListPaySlipByDateHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PaySlipDto>> Handle(GetListPaySlipByDate request, CancellationToken cancellationToken)
    {
        var PaySlips = await _context.PaySlips
                .Where(p => p.Paid_date >= request.fromDate && p.Paid_date <= request.toDate)
                .OrderByDescending(t => t.Paid_date)
                .ProjectTo<PaySlipDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

        return PaySlips;
    }
}
