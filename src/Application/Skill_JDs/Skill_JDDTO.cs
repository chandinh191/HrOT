using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Common;
using hrOT.Domain.Entities;


public class Skill_JDDTO : BaseAuditableEntity, IMapFrom<Skill_JD>
{
    [ForeignKey("Skill")]
    public Guid SkillId { get; set; }
    [ForeignKey("JobDescription")]
    public Guid JobDescriptionId { get; set; }
    public string Level { get; set; }
    // Relationship
    public virtual Skill Skill { get; set; }
    public virtual JobDescription JobDescription { get; set; }
}
