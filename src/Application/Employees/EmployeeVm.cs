using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;

namespace hrOT.Application.Employees;
public class EmployeeVm : IMapFrom<Employee>
{
    public Guid Id { get; set; }
    public string IdentityImage { get; set; }
    public DateTime BirthDay { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string BankName { get; set; }
    public string Fullname { get; set; }
    public string Address { get; set; }
    public string Image { get; set; }


    public string Diploma { get; set; }
    public string SelectedRole { get; set; } // New property to hold the selected role
}
