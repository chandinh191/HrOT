using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class Skill_Employee : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    [ForeignKey("Skill")]
    public Guid SkillId { get; set; }
    public string Level { get; set; }
    //Relationship

    public virtual Employee Employee { get; set; }
    public virtual Skill Skill { get; set; }
}
