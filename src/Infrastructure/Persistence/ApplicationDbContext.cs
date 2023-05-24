using System.Reflection;
using System.Reflection.Emit;
using Duende.IdentityServer.EntityFramework.Options;
using LogOT.Application.Common.Interfaces;
using LogOT.Domain.Entities;
using LogOT.Domain.Enums;
using LogOT.Domain.IdentityModel;

//using LogOT.Infrastructure.Identity;
using LogOT.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LogOT.Infrastructure.Persistence;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IMediator _mediator;
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options, operationalStoreOptions)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    //=================================================================================================

    public DbSet<TodoList> TodoLists => Set<TodoList>();
    public DbSet<TodoItem> TodoItems => Set<TodoItem>();
    public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
    public DbSet<Allowance> Allowances => Set<Allowance>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<CompanyContract> CompanyContracts => Set<CompanyContract>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<DetailTaxIncome> DetailTaxIncomes => Set<DetailTaxIncome>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeeContract> EmployeeContracts => Set<EmployeeContract>();
    public DbSet<Exchange> Exchanges => Set<Exchange>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Holiday> Holidays => Set<Holiday>();
    public DbSet<InterviewProcess> InterviewProcesses => Set<InterviewProcess>();
    public DbSet<JobDescription> JobDescriptions => Set<JobDescription>();
    public DbSet<LeaveLog> LeaveLogs => Set<LeaveLog>();
    public DbSet<Level> Levels => Set<Level>();
    public DbSet<OvertimeLog> OvertimeLogs => Set<OvertimeLog>();
    public DbSet<PaymentHistory> PaymentHistories => Set<PaymentHistory>();
    public DbSet<PaySlip> PaySlips => Set<PaySlip>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Skill_Employee> Skill_Employees => Set<Skill_Employee>();
    public DbSet<Skill_JD> Skill_JDs => Set<Skill_JD>();

    //=================================================================================================

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        //Seeding database
        builder.Entity<ApplicationUser>()
            .HasData(
            new ApplicationUser
            {
                Id = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                Fullname = "Lewis",
                Address = "TEST",
                Image = "TESTIMAGE",
                BirthDay = DateTime.Parse("9/9/9999"),
                UserName = "test",
                NormalizedUserName = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "098f6bcd4621d373cade4e832627b4f6",
                SecurityStamp = "test",
                ConcurrencyStamp = "test",
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = DateTimeOffset.Parse("9/9/9999 12:00:00 AM +07:00"),
                LockoutEnabled = false,
                AccessFailedCount = 0
            }
        );

        builder.Entity<Employee>()
            .HasData(
            new Employee
            {
                Id = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                ApplicationUserId = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                IdentityImage = "IMGTEST",
                Diploma = "TEST",
                BankAccountNumber = "123456789",
                BankAccountName = "LUONG THE DAN",
                BankName = "TECHCOMBANK",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );

        builder.Entity<Experience>()
                .HasData(
                    new Experience
                    {
                        Id = Guid.Parse("850df2d9-f8dc-444a-b1dc-ca773c0a2d0d"),
                        EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                        NameProject = "TestProject",
                        TeamSize = 4,
                        StartDate = DateTime.Parse("9/9/9999"),
                        EndDate = DateTime.Parse("9/9/9999"),
                        Description = "Normal",
                        TechStack = "MSSQL, .NET 7, MVC",
                        Status = true,
                        IsDeleted = false,
                        Created = DateTime.Parse("9/9/9999"),
                        CreatedBy = "test",
                        LastModified = DateTime.Parse("9/9/9999"),
                        LastModifiedBy = "test"
                    }
        );

        builder.Entity<EmployeeContract>()
        .HasData(
            new EmployeeContract
            {
                Id = Guid.Parse("42c05e21-2931-4d71-8735-1f17508621a7"),
                EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                File = "test",
                StartDate = new DateTime(9999, 9, 9, 0, 0, 0),
                EndDate = new DateTime(9999, 9, 9, 0, 0, 0),
                Job = "test",
                Salary = 1,
                Status = EmployeeContractStatus.Effective,
                SalaryType = SalaryType.Net,
                ContractType = ContractType.Open_Ended,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test",
                IsDeleted = false
            }
        );

        builder.Entity<LeaveLog>()
        .HasData(
            new LeaveLog
            {
                Id = Guid.Parse("93addd6c-7337-4b42-ab46-7f8b51405ea9"),
                EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                StartDate = new DateTime(9999, 9, 9, 0, 0, 0),
                EndDate = new DateTime(9999, 9, 9, 0, 0, 0),
                LeaveHours = 1,
                Reason = "test",
                Status = LeaveLogStatus.Pending,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            }
        );

        builder.Entity<Skill>()
        .HasData(
            new Skill
            {
                Id = Guid.Parse("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                SkillName = "TESTSKILL",
                Skill_Description = "TEST",
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test",
                IsDeleted = false
            }
        );

        builder.Entity<Skill_Employee>()
        .HasData(
            new Skill_Employee
            {
                Id = Guid.Parse("eaee3dcb-bfbb-497c-acff-6d013ef0ffd8"),
                EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                SkillId = Guid.Parse("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                Level = "Immediate",
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test",
                IsDeleted = false
            });

        builder.Entity<OvertimeLog>()
        .HasData(
            new OvertimeLog
            {
                Id = Guid.Parse("36cb5368-1b04-4b23-bdc0-2949ae3568d7"),
                EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                StartDate = new DateTime(9999, 9, 9, 0, 0, 0),
                EndDate = new DateTime(9999, 9, 9, 0, 0, 0),
                TotalHours = 9,
                Status = OvertimeLogStatus.Pending,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            });

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);
        return await base.SaveChangesAsync(cancellationToken);
    }
}