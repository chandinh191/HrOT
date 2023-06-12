using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.BankAccounts.Queries;
public class GetAllBankAccountQuery : IRequest<List<BankAccountDTO>>
{
}
public class GetAllBankAccountQueryHandler : IRequestHandler<GetAllBankAccountQuery, List<BankAccountDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetAllBankAccountQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<BankAccountDTO>> Handle(GetAllBankAccountQuery request, CancellationToken cancellationToken)
    {
        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);
        var employee = await _context.Employees.FindAsync(employeeId);

        var list = await _context.BankAccounts
            .Include(k => k.Bank)
            .Where(s => s.EmployeeId == employeeId && s.IsDeleted == false)
            .ProjectTo<BankAccountDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return list;
    }
}
