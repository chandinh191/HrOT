using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees_Skill.Queries;

public class Employee_GetListSkillQuery : IRequest<List<Skill_EmployeeDTO>>
{
    public Guid EmployeeId { get; set; }

    public Employee_GetListSkillQuery(Guid EmployeeID)
    {
        EmployeeId = EmployeeID;
    }
}

public class Employee_GetListSkillQueryHandler : IRequestHandler<Employee_GetListSkillQuery, List<Skill_EmployeeDTO>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetListSkillQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<Skill_EmployeeDTO>> Handle(Employee_GetListSkillQuery request, CancellationToken cancellationToken)
    {
        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;
        var employee = await _context.Employees.FindAsync(employeeId);

        var list = await _context.Skill_Employees
            .Include(k => k.Skill)
            .Where(s => s.EmployeeId == employeeId && s.IsDeleted == false)
            .ProjectTo<Skill_EmployeeDTO>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return list;
    }
}