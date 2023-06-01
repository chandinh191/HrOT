using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeExperience.Commands.Delete;

public class Employee_ExperienceDeleteCommand : IRequest<string>
{
    public Guid ExperienceID { get; set; }

    public Employee_ExperienceDeleteCommand(Guid ExperienceID)
    {
        this.ExperienceID = ExperienceID;
    }
}

public class Employee_ExperienceDeleteCommandHandler : IRequestHandler<Employee_ExperienceDeleteCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_ExperienceDeleteCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(Employee_ExperienceDeleteCommand request, CancellationToken cancellationToken)
    {

        //var employee = await _context.Employees
        //    .Where(e => e.Id == request.EmployeeID)
        //    .FirstOrDefaultAsync();
        //if (employee == null) { return "Id nhân viên không tồn tại"; }
        //if (employee.IsDeleted) { return "Nhân viên này đã bị xóa"; }


        var deleteExp = await _context.Experiences
            .Where(exp => exp.Id == request.ExperienceID)
            .FirstOrDefaultAsync();
        if (deleteExp == null) { return "Id kinh nghiệm không tồn tại"; }
        if (deleteExp.IsDeleted) { return "Kinh nghiệm này đã bị xóa"; }

        deleteExp.IsDeleted = true;
        _context.Experiences.Update(deleteExp);
        await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";
    }
}