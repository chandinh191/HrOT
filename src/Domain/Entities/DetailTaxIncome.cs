using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class DetailTaxIncome : BaseAuditableEntity
{
    [ForeignKey("PaySlip")]
    public Guid PaySlipId { get; set; }
    public string? Muc_chiu_thue { get; set; }
    public int? Thue_suat { get; set; }

    public virtual PaySlip PaySlip { get; set; } = null!;
}
