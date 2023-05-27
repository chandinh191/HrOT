using System.ComponentModel.DataAnnotations.Schema;
using hrOT.Application.Common.Mappings;
using hrOT.Application.EmployeeSkill;
using hrOT.Domain.Entities;

namespace hrOT.Application.Employees_Skill;

public class Skill_EmployeeDTO : IMapFrom<Skill_Employee>
{
    [ForeignKey("Skill")]
    public Guid SkillID { get; set; }
    public string Level { get; set; }

    //Relationship
    public virtual Skill? Skill { get; set; }
}