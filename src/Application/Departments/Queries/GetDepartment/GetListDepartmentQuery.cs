using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Common.Security;
using hrOT.Application.Levels;
using hrOT.Application.TodoLists.Queries.GetTodos;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Departments.Queries.GetDepartment;

public record GetListDepartmentQuery : IRequest<List<DepartmentDTO>>;

public class GetListDepartmentQueryHandler : IRequestHandler<GetListDepartmentQuery, List<DepartmentDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListDepartmentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDTO>> Handle(GetListDepartmentQuery request, CancellationToken cancellationToken)
    {
        return await _context.Departments
                 .ProjectTo<DepartmentDTO>(_mapper.ConfigurationProvider)
                 .ToListAsync(cancellationToken);
    }
}

