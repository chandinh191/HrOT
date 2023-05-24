using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class CompanyContract : BaseAuditableEntity
{
    [ForeignKey("Company")]
    public Guid CompanyId { get; set; }
    public decimal? Price { get; set; }
    public int? DayOff { get; set; }
    public string? ContactCode { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? UnitPrice { get; set; }
    public string? File { get; set; }

    // Relationship
    public virtual Company? Company { get; set; }
    public IList<PaymentHistory>? PaymentHistories { get; set; }

    public ICollection<Allowance>? Allowances { get; set; }
}
