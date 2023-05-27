using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LogOT.Application.Employees.Queries;

public record GetAllEmployeeQuery : IRequest<List<EmployeeDTO>>;

public class GetAllEmployeeQueryHandler : IRequestHandler<GetAllEmployeeQuery, List<EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAllEmployeeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeDTO>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
    {
        
        var list = await _context.Employees
           
            .Where(e => e.IsDeleted == false)
            .ProjectTo<EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }
}