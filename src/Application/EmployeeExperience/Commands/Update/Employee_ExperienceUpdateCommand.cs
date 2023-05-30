using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.EmployeeExperience.Commands;
using MediatR;

namespace hrOT.Application.Experiences.Commands;

public class Employee_ExperienceUpdateCommand : IRequest<string>
{
    public ExperienceCommandDTO Experience { get; set; }
    public Guid EmployeeID { get; set; }
    public Guid ExperienceID { get; set; }

    public Employee_ExperienceUpdateCommand(Guid ExperienceID, Guid EmployeeID, ExperienceCommandDTO experience)
    {
        Experience = experience;
        this.EmployeeID = EmployeeID;
        this.ExperienceID = ExperienceID;
    }
}

public class Employee_ExperienceUpdateCommandHandler : IRequestHandler<Employee_ExperienceUpdateCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_ExperienceUpdateCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(Employee_ExperienceUpdateCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employees
            .Where(e => e.Id == request.EmployeeID)
            .FirstOrDefault();

        if (employee != null)
        {
            var updateExp = _context.Experiences
            .Where(exp => exp.Id == request.ExperienceID)
            .FirstOrDefault();

            if (updateExp != null && updateExp.IsDeleted == false)
            {
                updateExp.NameProject = request.Experience.NameProject;
                updateExp.TeamSize = request.Experience.TeamSize;
                updateExp.StartDate = request.Experience.StartDate;
                updateExp.EndDate = request.Experience.EndDate;
                updateExp.Description = request.Experience.Description;
                updateExp.TechStack = request.Experience.TechStack;
                updateExp.Status = request.Experience.Status;
                updateExp.LastModifiedBy = employee.LastModifiedBy;

                _context.Experiences.Update(updateExp);
                await _context.SaveChangesAsync(cancellationToken);
                return "Cập nhật thành công";
            }
            else
            {
                return "Kinh nghiệm này đã bị xóa!";
            }
        }

        return "Cập nhật thất bại";
    }
}