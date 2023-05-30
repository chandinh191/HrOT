using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Domain.Enums;

namespace hrOT.Domain.Entities;
public class EmployeeContract : BaseAuditableEntity
{
    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public string? File { get; set; }
    
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? Salary { get; set; }
    public double? Number_Of_Dependents { get; set; }
    public double? CustomSalary { get; set; }
    public InsuranceType InsuranceType { get; set; }
    public EmployeeContractStatus? Status { get; set; }
    public SalaryType? SalaryType { get; set; }
    public ContractType? ContractType { get; set; }

    public virtual Employee Employee { get; set; }

    public IList<PaySlip> PaySlips { get; private set; }
    public ICollection<Allowance>? Allowances { get; set; }
}
