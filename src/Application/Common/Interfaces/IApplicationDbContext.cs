using hrOT.Domain.Entities;


using hrOT.Domain.Entities;


using hrOT.Domain.IdentityModel;
using Microsoft.EntityFrameworkCore;

namespace hrOT.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
   DbSet<ApplicationUser> ApplicationUsers { get; }

    DbSet<Allowance> Allowances { get; }
    DbSet<Company> Companies { get; }
    DbSet<CompanyContract> CompanyContracts { get; }
    DbSet<Department> Departments { get; }
    DbSet<DetailTaxIncome> DetailTaxIncomes { get; }
    DbSet<Employee> Employees { get; }
    DbSet<EmployeeContract> EmployeeContracts { get; }
    DbSet<Exchange> Exchanges { get; }
    DbSet<Experience> Experiences { get; }
    DbSet<Holiday> Holidays { get; }
    DbSet<InterviewProcess> InterviewProcesses { get; }
    DbSet<JobDescription> JobDescriptions { get; }
    DbSet<LeaveLog> LeaveLogs { get; }
    DbSet<Level> Levels { get; }
    DbSet<OvertimeLog> OvertimeLogs { get; }
    DbSet<PaymentHistory> PaymentHistories { get; }
    DbSet<PaySlip> PaySlips { get; }
    DbSet<Position> Positions { get; }
    DbSet<AnnualWorkingDay> AnnualWorkingDays { get; }
    DbSet<Skill> Skills { get; }
    DbSet<Skill_Employee> Skill_Employees { get; }
    DbSet<Skill_JD> Skill_JDs { get; }

    DbSet<TaxInCome> TaxInComes { get; }
    DbSet<TimeAttendanceLog> TimeAttendanceLogs { get; }

    DbSet<Degree> Degrees { get; }
    DbSet<Family> Families { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}