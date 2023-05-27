using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Employees.Queries;

public class Employee_GetByMatchingJobDescriptionSkillQuery : IRequest<List<EmployeeVm>>
{
    public string SkillName { get; set; }

    public Employee_GetByMatchingJobDescriptionSkillQuery(string SkillNAME)
    {
        SkillName = SkillNAME;
    }
}

public class Employee_GetByMatchingJobDescriptionSkillQueryHandler : IRequestHandler<Employee_GetByMatchingJobDescriptionSkillQuery, List<EmployeeVm>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_GetByMatchingJobDescriptionSkillQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<EmployeeVm>>? Handle(Employee_GetByMatchingJobDescriptionSkillQuery request, CancellationToken cancellationToken)
    {
        var employeeSkill = _context.Skill_Employees
            .Include(e => e.Skill)
            .Where(e => e.Skill.SkillName == request.SkillName)
            .FirstOrDefault();

        var jobSkill = _context.Skill_JDs
            .Include(j => j.Skill)
            .Where(j => j.Skill.SkillName == request.SkillName)
            .FirstOrDefault();

        if (employeeSkill == null || jobSkill == null)
        {
            return null;
        }
        else if (jobSkill.Skill.SkillName == employeeSkill.Skill.SkillName)
        {
            var employee = await _context.Employees
            .Where(e => e.Skill_Employees.Contains(employeeSkill))
            .ProjectTo<EmployeeVm>(_mapper.ConfigurationProvider)
            .ToListAsync();

            return employee;
        }

        return null;
    }
}