using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Departments;
using hrOT.Application.Departments.Queries.GetDepartment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Companys.Queries.GetCompany;

public record GetListCompanyQuery : IRequest<List<CompanyDTO>>;

public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, List<CompanyDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListCompanyQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CompanyDTO>> Handle(GetListCompanyQuery request, CancellationToken cancellationToken)
    {
        return await _context.Companies
                 .ProjectTo<CompanyDTO>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
    }
}

