using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.Experiences;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Add;

public class Employee_ExperienceCreateCommand : IRequest<bool>
{
    public ExperienceCommandDTO Experience { get; set; }
    public Guid EmployeeId { get; set; }

    public Employee_ExperienceCreateCommand(ExperienceCommandDTO experience, Guid EmployeeID)
    {
        Experience = experience;
        EmployeeId = EmployeeID;
    }

}

public class Employee_ExperienceCreateCommandHandler : IRequestHandler<Employee_ExperienceCreateCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_ExperienceCreateCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_ExperienceCreateCommand request, CancellationToken cancellationToken)
    {
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