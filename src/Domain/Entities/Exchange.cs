using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogOT.Domain.Entities;
public class Exchange : BaseAuditableEntity
{
    public double? Muc_Quy_Doi { get; set; }
}