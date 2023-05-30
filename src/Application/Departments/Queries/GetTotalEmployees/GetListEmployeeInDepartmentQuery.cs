using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Departments.Queries.GetTotalEmployees;

public class GetListEmployeeInDepartmentQuery : IRequest<int>
{
    public Guid DepartmentID { get; set; }

    public GetListEmployeeInDepartmentQuery(Guid DepartmentId)
    {
        DepartmentID = DepartmentId;
    }
}

public class GetListEmployeeInDepartmentQueryHandler : IRequestHandler<GetListEmployeeInDepartmentQuery, int>
{
    private readonly IApplicationDbContext _context;

    public GetListEmployeeInDepartmentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(GetListEmployeeInDepartmentQuery request, CancellationToken cancellationToken)
    {
        var employeeHolder = await _context.Departments
            .Where(d => d.Id == request.DepartmentID)
            .CountAsync(cancellationToken);

        return employeeHolder;
    }
}