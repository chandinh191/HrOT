using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Queries;

public class Staff_GetTotalEmployeeQuery : IRequest<int>
{
}

public class Staff_GetTotalEmployeeQueryHandler : IRequestHandler<Staff_GetTotalEmployeeQuery, int>
{
    private readonly IApplicationDbContext _context;

    public Staff_GetTotalEmployeeQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(Staff_GetTotalEmployeeQuery request, CancellationToken cancellationToken)
    {
        var totalEmployee = await _context.Employees
            .CountAsync(cancellationToken);

        return totalEmployee;
    }
}