using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Skills.Commands;
public class SkillCommandDTO : IMapFrom<Skill>
{
    public string SkillName { get; set; }

    public string Skill_Description { get; set; }
}
