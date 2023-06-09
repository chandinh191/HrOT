using AutoMapper;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.EmployeeSkill.Commands.Add;

public class Employee_CreateSkillCommand : IRequest<string>
{
    public Guid SkillId { get; set; }
    public Skills_EmployeeCommandDTO Skill_EmployeeDTO { get; set; }
    public Guid EmployeeId { get; set; }

    public Employee_CreateSkillCommand(Guid EmployeeID, Guid SkillID, Skills_EmployeeCommandDTO skill_EmployeeDTO)
    {
        SkillId = SkillID;
        Skill_EmployeeDTO = skill_EmployeeDTO;
        EmployeeId = EmployeeID;
    }
}

public class Employee_CreateSkillCommandHandler : IRequestHandler<Employee_CreateSkillCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Employee_CreateSkillCommandHandler(IApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> Handle(Employee_CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = await _context.Skills
            .Where(s => s.Id == request.SkillId)
            .FirstOrDefaultAsync();
        if (skill == null) { return "Id Kĩ năng không tồn tại!"; }
        if (skill.IsDeleted) { return "Kĩ năng này đã bị xóa!"; }

        //var employee = await _context.Employees
        //    .Where(e => e.Id == request.EmployeeId)
        //    .FirstOrDefaultAsync();
        //if (employee == null) { return "Id nhân viên không tồn tại!"; }
        //if (employee.IsDeleted) { return "Nhân viên này đã bị xóa!"; }

        //var employeeIdCookie = _httpContextAccessor.HttpContext.Request.Cookies["EmployeeId"];
        var employeeId = request.EmployeeId;

        var empskill = new Skill_Employee
        {
            Id = Guid.NewGuid(),
            Level = request.Skill_EmployeeDTO.Level,
            EmployeeId = employeeId,
            SkillId = request.SkillId
        };

        await _context.Skill_Employees.AddAsync(empskill);
        await _context.SaveChangesAsync(cancellationToken);
        return "Thêm thành công";
    }
}