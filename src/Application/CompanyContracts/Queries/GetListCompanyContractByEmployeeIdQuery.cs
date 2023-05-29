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

namespace hrOT.Application.CompanyContracts.Queries;
public record GetListCompanyContractByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<CompanyContractDto>>;
public class GetListCompanyContractByEmployeeIdQueryHandler : IRequestHandler<GetListCompanyContractByEmployeeIdQuery, List<CompanyContractDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListCompanyContractByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CompanyContractDto>> Handle(GetListCompanyContractByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var InterviewProcesses = await _context.InterviewProcesses          
            .Where(x => x.EmployeeId == request.EmployeeId && x.Result == InterviewProcessResult.Success)
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);
        return await _context.CompanyContracts
            .Where(x => InterviewProcesses.Contains(x.InterviewProcessId))
            .ProjectTo<CompanyContractDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}

