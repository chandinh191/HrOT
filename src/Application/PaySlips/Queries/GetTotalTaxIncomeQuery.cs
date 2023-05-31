using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Packaging.Ionic.Zip;

namespace hrOT.Application.PaySlips.Queries;
public record GetTotalTaxIncomeQuery(DateTime FromDate, DateTime ToDate) : IRequest<double?>;

public class GetTotalTaxIncomeQueryHandler : IRequestHandler<GetTotalTaxIncomeQuery, double?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTotalTaxIncomeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<double?> Handle(GetTotalTaxIncomeQuery request, CancellationToken cancellationToken)
    {

        double? totalTaxIncome = await _context.PaySlips
            .Where(x => !x.IsDeleted && x.Paid_date >= request.FromDate && x.Paid_date <= request.ToDate)
            .SumAsync(x => x.Tax_In_Come, cancellationToken);

        return totalTaxIncome;
    }

}


