using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Degrees;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace hrOT.Application.Allowances.Queries;

public record GetListAllowanceByEmployeeIdQuery(Guid EmployeeId) : IRequest<List<AllowanceDto>>
    
{
}
public class GetListDegreeByEmployeeIdQueryHandler : IRequestHandler<GetListAllowanceByEmployeeIdQuery, List<AllowanceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetListDegreeByEmployeeIdQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<AllowanceDto>> Handle(GetListAllowanceByEmployeeIdQuery request, CancellationToken cancellationToken)
    {
        var employeeId = request.EmployeeId;

        var list = await _context.Allowances
           .Include(a => a.EmployeeContract)
           .ThenInclude(b => b.Employee)
           .Where(b => b.EmployeeContract.Employee.Id == request.EmployeeId)
           .ProjectTo<AllowanceDto>(_mapper.ConfigurationProvider)
           .ToListAsync(cancellationToken);

        

        return list;
    }
}
