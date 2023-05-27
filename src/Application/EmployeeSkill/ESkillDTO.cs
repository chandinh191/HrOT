using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.EmployeeSkill;

public class ESkillDTO : IMapFrom<Skill>
{
    public Guid ID { get; set; }
    public string SkillName { get; set; }

    public string Skill_Description { get; set; }
}