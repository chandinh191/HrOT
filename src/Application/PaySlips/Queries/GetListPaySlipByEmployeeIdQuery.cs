using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Queries;
public record GetListPaySlipByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<PaySlipDto>>;
public class GetListPaySlipByEmployeeIdQueryHandler : IRequestHandler<GetListPaySlipByEmployeeIdQuery, List<PaySlipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListPaySlipByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PaySlipDto>> Handle(GetListPaySlipByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var EmployeeContract = await _context.EmployeeContracts
            .Where(x => x.EmployeeId == request.EmployeeId && x.Status == EmployeeContractStatus.Effective)
            .SingleOrDefaultAsync(cancellationToken);
        var PaySlips = await _context.PaySlips
                .Where(x => x.EmployeeContractId == EmployeeContract.Id)
                .OrderByDescending(t => t.Paid_date)
                .ProjectTo<PaySlipDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        foreach (var PaySlip in PaySlips)
        {
            PaySlip.DetailTaxIncomes = PaySlip.DetailTaxIncomes.OrderBy(x => x.Level).ToList();
        }

        return PaySlips;
    }
}
