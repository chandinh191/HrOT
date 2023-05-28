using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Delete;

public class Employee_ExperienceDeleteCommand : IRequest<bool>
{
    public Guid EmployeeID { get; set; }
    public Guid ExperienceID { get; set; }

    public Employee_ExperienceDeleteCommand(Guid ExperienceID, Guid EmployeeID)
    {
        this.EmployeeID = EmployeeID;
        this.ExperienceID = ExperienceID;
    }
}

public class Employee_ExperienceDeleteCommandHandler : IRequestHandler<Employee_ExperienceDeleteCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_ExperienceDeleteCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> Handle(Employee_ExperienceDeleteCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == request.EmployeeID)
            .FirstOrDefaultAsync();

        if (employee != null)
        {
            var deleteExp = await _context.Experiences
                .Where(exp => exp.Id == request.ExperienceID)
                .FirstOrDefaultAsync();

            if (deleteExp != null)
            {
                deleteExp.IsDeleted = true;
                _context.Experiences.Update(deleteExp);
                await _context.SaveChangesAsync(cancellationToken);
                return true;
            }
        }

        return false;
    }
}