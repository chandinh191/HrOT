using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Add;

public class Employee_ExperienceCreateCommand : IRequest<string>
{
    public ExperienceCommandDTO Experience { get; set; }



    public Employee_ExperienceCreateCommand(ExperienceCommandDTO experience)
    {
        Experience = experience;

    }
}

public class Employee_ExperienceCreateCommandHandler : IRequestHandler<Employee_ExperienceCreateCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_ExperienceCreateCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(Employee_ExperienceCreateCommand request, CancellationToken cancellationToken)
    {

        if (request.Experience.StartDate.Year > 9999 || request.Experience.StartDate.Year <= 1990) { return "Năm bắt đầu phải nằm giữa 1990 và 9999"; }
        if (request.Experience.EndDate.Year > 9999 || request.Experience.EndDate.Year <= 1990) { return "Năm kết thúc phải nằm giữa 1990 và 9999"; }
        if (request.Experience.StartDate > request.Experience.EndDate) { return "Ngày bắt đầu phải sớm hơn ngày kết thúc."; }
        if (request.Experience.EndDate < request.Experience.StartDate) { return "Ngày kết thúc phải sau ngày bắt đầu."; }

        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);

        //var employee = await _context.Employees
        //    .Include(a => a.ApplicationUser)
        //    .Where(e => e.Id == request.EmployeeId)
        //    .FirstOrDefaultAsync();
        //if (employee == null) { return "Id nhân viên không tồn tại"; }
        //if (employee.IsDeleted) { return "Nhân viên này đã bị xóa"; }


        var experience = new Experience
        {
            Id = new Guid(),
            EmployeeId = employeeId,
            NameProject = request.Experience.NameProject,
            TeamSize = request.Experience.TeamSize,
            StartDate = request.Experience.StartDate,
            EndDate = request.Experience.EndDate,
            Description = request.Experience.Description,
            TechStack = request.Experience.TechStack,
            Status = request.Experience.Status,
        };

        await _context.Experiences.AddAsync(experience);
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}