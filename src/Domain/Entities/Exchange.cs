using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class Exchange : BaseAuditableEntity
{
    public double? Muc_Quy_Doi { get; set; }
    public double? Giam_Tru { get; set; }
    public double? Thue_Suat { get; set; }
}