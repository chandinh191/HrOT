using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommand : IRequest<string>
{
    public Skills_EmployeeCommandDTO _dto;
    public Guid SkillId { get; set; }

    public Guid EmployeeId { get; set; }

    public Employee_UpdateSkillCommand(Guid EmployeeID, Guid SKillID, Skills_EmployeeCommandDTO dto)
    {
        _dto = dto;
        SkillId = SKillID;
        EmployeeId = EmployeeID;
    }
}

public class Employee_UpdateSkillCommandHandler : IRequestHandler<Employee_UpdateSkillCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_UpdateSkillCommandHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(Employee_UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id Kĩ năng không tồn tại!"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;

        var empSkill = await _context.Skill_Employees
            .Where(es => es.EmployeeId == employeeId
            && es.SkillId == request.SkillId)
            .FirstOrDefaultAsync();
        if (empSkill == null) { return "Id Nhân viên không tồn tại!"; }
        if (empSkill.IsDeleted) { return "Kĩ năng này của nhiên viên đã bị xóa!"; }

        empSkill.Level = request._dto.Level;
        empSkill.SkillId = request.SkillId;

        _context.Skill_Employees.Update(empSkill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Cập nhật thành công";
    }
}