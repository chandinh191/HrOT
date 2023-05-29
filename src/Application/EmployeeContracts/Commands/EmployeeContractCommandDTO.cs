using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace hrOT.Application.EmployeeContracts.Commands;
public class EmployeeContractCommandDTO : IMapFrom<EmployeeContract>
{
    public IFormFile File { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Job { get; set; }
    public double? Salary { get; set; }
    public double? CustomSalary { get; set; }
    public double? Number_Of_Dependents { get; set; }
    public EmployeeContractStatus? Status { get; set; }

    public InsuranceType InsuranceType { get; set; }
    public SalaryType SalaryType { get; set; }
    public ContractType ContractType { get; set; }
}
