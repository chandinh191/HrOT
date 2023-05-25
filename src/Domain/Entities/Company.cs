using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hrOT.Domain.Entities;
public class Company : BaseAuditableEntity
{
   
    public string Name { get; set; }
    public string Address { get; set; }
    public string AccountEmail { get; set; }
    public string Phone { get; set; }
    public string HREmail { get; set; }

    // Relationship
    public IList<Holiday> Holidays { get; set; }
    public IList<JobDescription> JobDescriptions { get; set; }
    public IList<CompanyContract> CompanyContracts { get; set; }

}
