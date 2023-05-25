using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class PaymentHistory : BaseAuditableEntity
{

    [ForeignKey("CompanyContract")]
    public Guid CompanyContractId { get; set; }
    public decimal? Total { get; set; }
    public decimal? Tax { get; set; }
    public string? Note { get; set; }
    public decimal? TotalDeduction { get; set; }
    public DateTime? Date { get; set; }
    public decimal? TotalBonus { get; set; }
    public string? AcceptanceCode { get; set;}
    public string? Status { get; set; }

    //relationship
    public virtual CompanyContract CompanyContract { get; set; } = null!;
}
