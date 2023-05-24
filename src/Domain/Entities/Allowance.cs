using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogOT.Domain.Entities;

public class Allowance : BaseAuditableEntity
{
    [ForeignKey("CompanyContract")]
    public Guid CompanyContractId { get; set; }

    public string Name { get; set; }
    public AllowanceType Type { get; set; }
    public decimal Amount { get; set; }

    // Tiêu chí đủ điều kiện nhận trợ cấp
    [Required]
    public string Eligibility_Criteria { get; set; }

    // Yêu cầu tài liệu phụ cấp
    [Required]
    public string Requirements { get; set; }

    // Relationship
    public virtual CompanyContract? CompanyContract { get; set; }
}