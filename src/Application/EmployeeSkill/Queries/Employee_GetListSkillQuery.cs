using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees_Skill.Queries;

public class Employee_GetListSkillQuery : IRequest<List<Skill_EmployeeDTO>>
{
    public Guid Id { get; set; }

    public Employee_GetListSkillQuery(Guid id)
    {
        Id = id;
    }
}

public class Employee_GetListSkillQueryHandler : IRequestHandler<Employee_GetListSkillQuery, List<Skill_EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_GetListSkillQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Skill_EmployeeDTO>> Handle(Employee_GetListSkillQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Skill_Employees
            .Include(k => k.Skill)
            .Where(s => s.EmployeeId.Equals(request.Id) && s.IsDeleted == false)
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return list;
    }
}