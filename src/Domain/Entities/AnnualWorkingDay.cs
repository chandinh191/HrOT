using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class AnnualWorkingDay : BaseAuditableEntity
{
    public DateTime Day { get; set; }
    public double Coefficients { get; set; }
   
    public TypeDate TypeDate { get; set; }
}
