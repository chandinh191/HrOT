using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class PaySlip : BaseAuditableEntity
{

    [ForeignKey("EmployeeContract")]
    public Guid EmployeeContractId { get; set; }
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
    public double? Company_Paid { get; set; }
    public DateTime?  Paid_date { get; set; }
    public string? Note { get; set; }
    public string? BankName { get; set; }
    public string? BankAcountName { get; set; }
    public int? BankAcountNumber { get; set; }

    public virtual EmployeeContract EmployeeContract { get; set; } = null!;

    public ICollection<DetailTaxIncome> DetailTaxIncomes { get; set; }
}
