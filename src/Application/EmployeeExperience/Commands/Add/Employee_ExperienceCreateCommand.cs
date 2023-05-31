﻿using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Experiences;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Add;

public class Employee_ExperienceCreateCommand : IRequest<bool>
{
    public ExperienceCommandDTO Experience { get; set; }
    public Guid EmployeeId { get; set; }

    public Employee_ExperienceCreateCommand(ExperienceCommandDTO experience)
    {
        Experience = experience;
       
    }

}

public class Employee_ExperienceCreateCommandHandler : IRequestHandler<Employee_ExperienceCreateCommand, bool>
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

    public async Task<bool> Handle(Employee_ExperienceCreateCommand request, CancellationToken cancellationToken)
    {
        if (request.EmployeeId == null)
        {
            // Lấy Id từ cookie
            var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
            request.EmployeeId = Guid.Parse(employeeIdCookie);
        }
        var employee = await _context.Employees
            .Include(a => a.ApplicationUser)
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefaultAsync();

        if (employee != null)
        {
            var experience = new Experience
            {
                Id = new Guid(),
                EmployeeId = request.EmployeeId,
                NameProject = request.Experience.NameProject,
                TeamSize = request.Experience.TeamSize,
                StartDate = request.Experience.StartDate,
                EndDate = request.Experience.EndDate,
                Description = request.Experience.Description,
                TechStack = request.Experience.TechStack,
                Status = request.Experience.Status,
                CreatedBy = employee.ApplicationUser.UserName
            };

            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}