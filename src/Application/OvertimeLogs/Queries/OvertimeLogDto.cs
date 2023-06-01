using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using hrOT.Application.Common.Mappings;

namespace hrOT.Application.OvertimeLogs.Queries;
public class OvertimeLogDto : IMapFrom<OvertimeLog>
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    // Tổng số giờ làm việc
    public double TotalHours { get ; set; }

    // Trạng thái kiểm duyệt
    public OvertimeLogStatus Status { get; set; }

}
