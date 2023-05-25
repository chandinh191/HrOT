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
    public EmployeeContractStatus? Status { get; set; }

    public SalaryType? SalaryType { get; set; }
    public ContractType? ContractType { get; set; }

    // Custom method
    public string GetStatusName()
    {
        var name = Enum.GetName(typeof(EmployeeContractStatus), Status);
        return name;
    }
}