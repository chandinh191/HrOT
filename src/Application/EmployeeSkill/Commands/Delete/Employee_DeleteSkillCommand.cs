using AutoMapper;
using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Delete;

public class Employee_DeleteSkillCommand : IRequest<string> { 

    public Guid SkilLId { get; set; }

    public Employee_DeleteSkillCommand( Guid SkilLID) { 
    
        SkilLId = SkilLID;
    }
}

public class Employee_DeleteSkillCommandHandler : IRequestHandler<Employee_DeleteSkillCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_DeleteSkillCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(Employee_DeleteSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkilLId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id kĩ năng không tồn tại"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa"; }

        var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = Guid.Parse(employeeIdCookie);

        var empSkill = await _context.Skill_Employees
            .Where(es => es.EmployeeId == employeeId
            && es.SkillId == request.SkilLId)
            .FirstOrDefaultAsync();
        if (empSkill == null) { return "Id nhân viên không tồn tại"; }
        if (empSkill.IsDeleted) { return "Kĩ năng này của nhân viên đã bị xóa"; }

        empSkill.IsDeleted = true;

        _context.Skill_Employees.Update(empSkill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Xóa thành công";
    }
}