using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;

public class TaxInCome : BaseAuditableEntity

{
    public double? Muc_chiu_thue { get; set; }
    public double? Thue_suat { get; set; }
}
