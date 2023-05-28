using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.InterviewProcesses.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.CompanyContracts.Queries;
public record GetListCompanyContractQuery : IRequest<List<CompanyContractDto>>;
public class GetListCompanyContractQueryHandler : IRequestHandler<GetListCompanyContractQuery, List<CompanyContractDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListCompanyContractQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CompanyContractDto>> Handle(GetListCompanyContractQuery request, CancellationToken cancellationToken)
    {
        return await _context.CompanyContracts
            .OrderBy(x => x.StartDate)
            .ProjectTo<CompanyContractDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

