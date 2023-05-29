using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.PaySlips.Queries;
public record GetTotalSalaryPayForEmployeeQuery(DateTime FromDate, DateTime ToDate) : IRequest<double?>;
public class GetTotalSalaryPayForEmployeeQueryHandler : IRequestHandler<GetTotalSalaryPayForEmployeeQuery, double?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTotalSalaryPayForEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<double?> Handle(GetTotalSalaryPayForEmployeeQuery request, CancellationToken cancellationToken)
    {
        return await _context.PaySlips
            .Where(x => x.Paid_date >= request.FromDate && x.Paid_date <= request.ToDate)
            .SumAsync(x => x.Company_Paid, cancellationToken);
    }
}
