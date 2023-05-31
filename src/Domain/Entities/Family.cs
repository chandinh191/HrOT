using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace hrOT.Domain.Entities;
public class Family: BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    public string? HomeTown { get; set; }
    public virtual Employee Employee { get; set; }
}
