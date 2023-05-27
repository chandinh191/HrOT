using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Skills.Queries;

public class SkillDTO : IMapFrom<Skill>
{
    public Guid ID { get; set; }

    public string SkillName { get; set; }

    public string Skill_Description { get; set; }
}