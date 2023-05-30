using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Exceptions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.TodoLists.Queries.GetTodos;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.Employees.Queries;
public record Employee_GetEmployeeQuery : IRequest<EmployeeVm>
{
    
}

public class Employee_GetEmployeeQueryHandler : IRequestHandler<Employee_GetEmployeeQuery, EmployeeVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<EmployeeVm> Handle(Employee_GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);
        var employee = await _context.Employees.FindAsync(employeeId);

        if (employee == null || employee.IsDeleted)
        {
            throw new NotFoundException("Employee not found");
        }

        var employeeVm = _context.Employees
            .Where(e => e.Id == employeeId)
            .ProjectTo<EmployeeVm>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        return employeeVm;
    }
}
