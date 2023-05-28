using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class CompanyContract : BaseAuditableEntity
{
    [ForeignKey("InterviewProcess")]
    public Guid InterviewProcessId { get; set; }
    public string? File { get; set; }
    public double? Salary { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; } 
    public CompanyContractStatus? Status { get; set; }

    // Relationship
    public virtual InterviewProcess? InterviewProcess { get; set; }
    public IList<PaymentHistory>? PaymentHistories { get; set; }

}
