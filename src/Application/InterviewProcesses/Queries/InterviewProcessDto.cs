using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using hrOT.Application.Common.Mappings;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;

namespace hrOT.Application.InterviewProcesses.Queries;
public class InterviewProcessDto : IMapFrom<InterviewProcess>
{
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public DateTime DayTime { get; set; }
    public string Place { get; set; }
    public string FeedBack { get; set; }
    public InterviewProcessResult Result { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InterviewProcess, InterviewProcessDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.ApplicationUser.Fullname));
    }
}
