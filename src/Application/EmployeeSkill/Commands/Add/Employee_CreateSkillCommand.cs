using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommand : IRequest<string>
{
    public Guid EmployeeId { get; set; }
    public Guid SkillId { get; set; }
    public Skills_EmployeeCommandDTO Skill_EmployeeDTO { get; set; }

    public Employee_CreateSkillCommand(Guid EmployeeID, Guid SkillID, Skills_EmployeeCommandDTO skill_EmployeeDTO)
    {
        EmployeeId = EmployeeID;
        SkillId = SkillID;
        Skill_EmployeeDTO = skill_EmployeeDTO;
    }
}

public class Employee_CreateSkillCommandHandler : IRequestHandler<Employee_CreateSkillCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public Employee_CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<string> Handle(Employee_CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id Kĩ năng không tồn tại!"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        var employee = await _context.Employees
            .Where(e => e.Id == request.EmployeeId)
            .FirstOrDefaultAsync();
        if (employee == null) { return "Id nhân viên không tồn tại!"; }
        if (employee.IsDeleted) { return "Nhân viên này đã bị xóa!"; }

        var empskill = new Skill_Employee
        {
            Id = Guid.NewGuid(),
            Level = request.Skill_EmployeeDTO.Level,
            EmployeeId = request.EmployeeId,
            SkillId = request.SkillId
        };

        await _context.Skill_Employees.AddAsync(empskill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}