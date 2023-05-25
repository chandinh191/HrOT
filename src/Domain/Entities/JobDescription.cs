using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class JobDescription : BaseAuditableEntity
{
    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public virtual Company Company { get; set; }

    public ICollection<Skill_JD> Skill_JDs { get; set; }
}
