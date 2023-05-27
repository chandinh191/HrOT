using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.Allowances.Queries;
public class AllowanceDto : IMapFrom<Allowance>
{
    public Guid Id { get; set; }
    public Guid EmployeeContractId { get; set; }

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
    public bool IsDeleted { get; set; }
    public virtual EmployeeContract? EmployeeContract { get; set; }
}

