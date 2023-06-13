using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeContracts.Queries;
public class Employee_GetSeniorityContractQuery : IRequest<double>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetSeniorityContractQuery(Guid EmployeeID)
    {
        EmployeeId = EmployeeID;
    }
}
public class Employee_GetSeniorityContractQueryHandler : IRequestHandler<Employee_GetSeniorityContractQuery, double>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public Employee_GetSeniorityContractQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<double> Handle(Employee_GetSeniorityContractQuery request, CancellationToken cancellationToken)
    {
        /*var list = await _context.EmployeeContracts
            .Where(ec => ec.EmployeeId == request.EmployeeId && ec.IsDeleted == false)
            .ProjectTo<EmployeeContractDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;*/
        var list = await _context.EmployeeContracts
            .Where(ec => ec.EmployeeId == request.EmployeeId && ec.IsDeleted == false)
            .ProjectTo<EmployeeContractDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        var result = list.Sum(x => x.GetContractLength()) / 365;
        return Math.Round(result, 3);
    }
}
