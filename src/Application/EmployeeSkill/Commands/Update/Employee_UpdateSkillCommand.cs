using hrOT.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Update;

public class Employee_UpdateSkillCommand : IRequest<string>
{
    public Skills_EmployeeCommandDTO _dto;
    public Guid EmployeeId { get; set; }
    public Guid SkillId { get; set; }

    public Employee_UpdateSkillCommand(Guid EmployeeID, Guid SKillID, Skills_EmployeeCommandDTO dto)
    {
        _dto = dto;
        EmployeeId = EmployeeID;
        SkillId = SKillID;
    }
}

public class Employee_UpdateSkillCommandHandler : IRequestHandler<Employee_UpdateSkillCommand, string>
{
    private readonly IApplicationDbContext _context;

    public Employee_UpdateSkillCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> Handle(Employee_UpdateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id Kĩ năng không tồn tại!"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        var empSkill = await _context.Skill_Employees
            .Where(es => es.EmployeeId == request.EmployeeId
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