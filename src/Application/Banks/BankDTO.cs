using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Common;
using hrOT.Domain.Entities;

namespace hrOT.Application.Banks;
public class BankDTO : BaseAuditableEntity, IMapFrom<Bank>
{
    public string BankName { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; }

    // Relationship
    public IList<BankAccount> BankAccounts { get; private set; }
}
