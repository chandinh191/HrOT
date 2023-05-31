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
    public double Amount { get; set; }

    public string Eligibility_Criteria { get; set; }

    public string Requirements { get; set; }

}

