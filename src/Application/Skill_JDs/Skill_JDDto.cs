using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Skill_JDs;
public class Skill_JDDto : IMapFrom<Skill_JD>
{
    
    public Guid SkillId { get; set; }
    
    public Guid JobDescriptionId { get; set; }
    public string Level { get; set; }
    public bool IsDeleted { get; set; }
}
