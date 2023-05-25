using hrOT.Application.Common.Mappings;
using hrOT.Application.EmployeeSkill;
using hrOT.Domain.Entities;

namespace hrOT.Application.Employees_Skill;

public class Skill_EmployeeDTO : IMapFrom<Skill_Employee>
{
    public string Level { get; set; }

    //Relationship
    public virtual ESkillDTO? Skill { get; set; }
}