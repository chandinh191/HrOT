using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeContracts.Queries;

public class Employee_GetListContractQuery : IRequest<List<EmployeeContractDTO>>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetListContractQuery(Guid EmployeeID)
    {
        EmployeeId = EmployeeID;
    }
}

public class Employee_GetListContractQueryHandler : IRequestHandler<Employee_GetListContractQuery, List<EmployeeContractDTO>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public Employee_GetListContractQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeContractDTO>> Handle(Employee_GetListContractQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.EmployeeContracts
            .Where(ec => ec.EmployeeId == request.EmployeeId && ec.IsDeleted == false)
            .ProjectTo<EmployeeContractDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return list;
    }
}