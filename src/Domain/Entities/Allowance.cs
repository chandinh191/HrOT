using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hrOT.Domain.Entities;

public class Allowance : BaseAuditableEntity
{
    [ForeignKey("EmployeeContract")]
    public Guid EmployeeContractId { get; set; }

    public string Name { get; set; }
    public AllowanceType Type { get; set; }
    public double Amount { get; set; }

    // Tiêu chí đủ điều kiện nhận trợ cấp
    [Required]
    public string Eligibility_Criteria { get; set; }

    // Yêu cầu tài liệu phụ cấp
    [Required]
    public string Requirements { get; set; }

    // Relationship
    //public virtual CompanyContract? CompanyContract { get; set; }
    public virtual EmployeeContract? EmployeeContract { get; set; }
}