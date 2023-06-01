using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Application.EmployeeExperience.Commands;
using MediatR;

namespace hrOT.Application.Experiences.Commands;

public class Employee_ExperienceUpdateCommand : IRequest<string>
{
    public ExperienceCommandDTO Experience { get; set; }

    //public Guid EmployeeID { get; set; }
    public Guid ExperienceID { get; set; }

    public Employee_ExperienceUpdateCommand(Guid experienceID, ExperienceCommandDTO experience)
    {
        Experience = experience;
        this.ExperienceID = experienceID;
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
        if (request.Experience.StartDate.Year > 9999 || request.Experience.StartDate.Year <= 1990) { return "Năm bắt đầu phải nằm giữa 1990 và 9999"; }
        if (request.Experience.EndDate.Year > 9999 || request.Experience.EndDate.Year <= 1990) { return "Năm kết thúc phải nằm giữa 1990 và 9999"; }
        if (request.Experience.StartDate > request.Experience.EndDate) { return "Ngày bắt đầu phải sớm hơn ngày kết thúc."; }
        if (request.Experience.EndDate < request.Experience.StartDate) { return "Ngày kết thúc phải sau ngày bắt đầu."; }

        var updateExp = _context.Experiences
        .Where(exp => exp.Id == request.ExperienceID)
        .FirstOrDefault();
        if (updateExp == null) { return "Id kinh nghiệm không tồn tại"; }
        if (updateExp.IsDeleted) { return "Kinh nghiệm này đã bị xóa"; }

        updateExp.NameProject = request.Experience.NameProject;
        updateExp.TeamSize = request.Experience.TeamSize;
        updateExp.StartDate = request.Experience.StartDate;
        updateExp.EndDate = request.Experience.EndDate;
        updateExp.Description = request.Experience.Description;
        updateExp.TechStack = request.Experience.TechStack;
        updateExp.Status = request.Experience.Status;

        _context.Experiences.Update(updateExp);
        await _context.SaveChangesAsync(cancellationToken);
        return "Cập nhật thành công";
    }
}