using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class DetailTaxIncome : BaseAuditableEntity
{
    [ForeignKey("PaySlip")]
    public Guid PaySlipId { get; set; }
    public int? Level { get; set; }
    public double? Payment { get; set; }
    public virtual PaySlip PaySlip { get; set; } = null!;
}
