using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.PaySlips;
public class PaySlipDto : IMapFrom<PaySlip>
{
    public int? Standard_Work_Hours { get; set; }
    public int? Actual_Work_Hours { get; set; }
    public int? Ot_Hours { get; set; }
    public int? Leave_Hours { get; set; }
    public double? Salary { get; set; }
    public double? BHXH_Emp { get; set; }
    public double? BHYT_Emp { get; set; }
    public double? BHTN_Emp { get; set; }
    public double? BHXH_Comp { get; set; }
    public double? BHYT_Comp { get; set; }
    public double? BHTN_Comp { get; set; }
    public double? Tax_In_Come { get; set; }
    public double? Bonus { get; set; }
    public double? Deduction { get; set; }
    public double? Total_Allowance { get; set; }
    public double? Final_Salary { get; set; }
    public DateTime? Paid_date { get; set; }
    public IList<DetailTaxInComeDto> DetailTaxIncomes { get; set; }
}
