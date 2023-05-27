using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.EmployeeContracts;

public class EmployeeContractDTO : IMapFrom<EmployeeContract>
{
    public Guid Id { get; set; }
    public string? File { get; set; }
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

    // Custom method    

    public string GetStatusName()
    {
        var name = Enum.GetName(typeof(EmployeeContractStatus), Status);
        return name;
    }

    public string GetSalaryName()
    {
        var name = Enum.GetName(typeof(SalaryType), SalaryType);
        return name;
    }

    public string GetContractName()
    {
        var name = Enum.GetName(typeof(ContractType), ContractType);
        return name;
    }

    public string GetInsuranceName()
    {
        var name = Enum.GetName(typeof(InsuranceType), InsuranceType);
        return name;
    }
}