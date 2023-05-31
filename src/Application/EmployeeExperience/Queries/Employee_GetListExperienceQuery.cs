using AutoMapper;
using AutoMapper.QueryableExtensions;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Experiences.Queries;

public class Employee_GetListExperienceQuery : IRequest<List<ExperienceDTO>>
{
    //public EmployeeDTO GetEmployee { get; set; }
    public Guid Id { get; set; }

    public Employee_GetListExperienceQuery(Guid id)
    {
        Id = id;
    }
}

public class Employee_GetListExperienceQueryHandler : IRequestHandler<Employee_GetListExperienceQuery, List<ExperienceDTO>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_GetListExperienceQueryHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
   

    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<ExperienceDTO>> Handle(Employee_GetListExperienceQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == null)
        {
            // Lấy Id từ cookie
            var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
            request.Id = Guid.Parse(employeeIdCookie);
        }
        var list = await _context.Experiences
            .Where(exp => exp.EmployeeId.Equals(request.Id) && exp.IsDeleted == false)
            .ProjectTo<ExperienceDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return list;
    }
}