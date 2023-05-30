using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Application.Common.Mappings;
using hrOT.Application.Experiences;
using hrOT.Domain.Common;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;
using hrOT.Application.Common.Mappings;

using hrOT.Domain.Common;
using hrOT.Domain.Entities;
using hrOT.Domain.IdentityModel;

namespace LogOT.Application.Employees;

public class EmployeeDTO : BaseAuditableEntity, IMapFrom<Employee>
{
    [ForeignKey("ApplicationUser")]
    public string ApplicationUserId { get; set; }

    // Bằng cấp
    //public string? Diploma { get; set; }

    // Thẻ căn cước
    public string? CitizenIdentificationNumber { get; set; }
    public DateTime? CreatedDateCIN { get; set; }
    public string? PlaceForCIN { get; set; }
    // Ngân Hàng
    public string BankName { get; set; }
    public string BankAccountNumber { get; set; }
    public string BankAccountName { get; set; }
    public string? CVPath { get; set; }

    //Địa chỉ
    public string? Address { get; set; }
    public string? District { get; set; }
    public string? Province { get; set; }

    // Relationship
    public IList<Experience> Experiences { get; private set; }

    public IList<OvertimeLog> OvertimeLogs { get; private set; }
    public IList<LeaveLog> LeaveLogs { get; private set; }

    public ICollection<Department> Departments { get; set; }
    public IList<EmployeeContract> EmployeeContracts { get; private set; }
    public IList<InterviewProcess> InterviewProcesses { get; private set; }
    public IList<Skill_Employee> Skill_Employees { get; private set; }

    public virtual ApplicationUser ApplicationUser { get; set; }
    public IList<TimeAttendanceLog> TimeAttendanceLogs { get; private set; }

    public IList<Degree> Degrees { get; private set; }
}