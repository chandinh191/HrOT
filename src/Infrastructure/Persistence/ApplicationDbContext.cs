using System.Reflection;
using Duende.IdentityServer.EntityFramework.Options;
using hrOT.Application.Common.Interfaces;
using hrOT.Domain.Entities;
using hrOT.Domain.Enums;
using hrOT.Domain.IdentityModel;

//using hrOT.Infrastructure.Identity;
using hrOT.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hrOT.Infrastructure.Persistence;

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

    public DbSet<TaxInCome> TaxInComes => Set<TaxInCome>();
    public DbSet<TimeAttendanceLog> TimeAttendanceLogs => Set<TimeAttendanceLog>();
    public DbSet<Family> Families => Set<Family>();
    public DbSet<Degree> Degrees => Set<Degree>();

    public DbSet<AnnualWorkingDay> AnnualWorkingDays => Set<AnnualWorkingDay>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<ApplicationUser>()
            .HasData(
            new ApplicationUser
            {
                Id = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                Fullname = "Lewis",
                Image = "TESTIMAGE",
                UserName = "admin",
                BirthDay = DateTime.Parse("9/9/9999"),
                NormalizedUserName = "test",
                Email = "test@gmail.com",
                NormalizedEmail = "test@gmail.com",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEFNwXlIXp0mbDE5k1gIQdlbAczn8BwINQnF5S0qULxDK/6luT/bumpD+HFOXM0k59A==",
                SecurityStamp = "VEPOTJNXQCZMK3J7R27HMLXD64T72GU6",
                ConcurrencyStamp = "40495f9c-e853-41e8-8c5b-6b3c93d3791b",
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = DateTimeOffset.Parse("9/9/9999 12:00:00 AM +07:00"),
                LockoutEnabled = true,
                AccessFailedCount = 0
            }
        );

        builder.Entity<Department>()
            .HasData(
            new Department
            {
                Id = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894142"),            
                Name = "Phòng IT",
                Description = "Đảm nhận công việc liên quan phần mềm",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );

        builder.Entity<Position>()
            .HasData(
            new Position
            {
                Id = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894143"),
                DepartmentId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894142"),
                Name = "Nhân viên",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );

        builder.Entity<Employee>()
            .HasData(
            new Employee
            {
                Id = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                ApplicationUserId = "fe30e976-2640-4d35-8334-88e7c3b1eac1",
                PositionId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894143"),
                CitizenIdentificationNumber = "0931248141241231",
                CreatedDateCIN = DateTime.Parse("1/1/2023"),
                PlaceForCIN = "TP HCM",
                Address = "123, Lê Văn Việt, Tăng Nhơn Phú",
                District = "Quận nine",
                Province = "TP Hồ Chí Minh",
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
        builder.Entity<Family>()
            .HasData(
            new Family
            {
                Id = Guid.Parse("668d6b8b-7997-40fc-9454-036158af413b"),
                EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
                FatherName = "Test",
                MotherName = "Test",
                NumberOfDependents = 3,
                HomeTown = "Test",
                Created = DateTime.Parse("9/9/9999"),
                CreatedBy = "Test",
                LastModified = DateTime.Parse("9/9/9999"),
                LastModifiedBy = "Test",
                IsDeleted = false
            }
        );
        builder.Entity<Degree>()
           .HasData(
           new Degree
           {
               Id = Guid.Parse("d10de52b-58a1-43e8-9ab6-5651693341f8"),
               EmployeeId = Guid.Parse("ac69dc8e-f88d-46c2-a861-c9d5ac894141"),
               Name = "Test",
               Status = DegreeStatus.Valid,
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
                Salary = 20000000,
                Number_Of_Dependents = 0,
                InsuranceType = InsuranceType.Official,
                CustomSalary = 0,
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
        builder.Entity<Allowance>()
       .HasData(
           new Allowance
           {
               Id = Guid.Parse("c0d544cb-a345-490d-8ba3-d1c63e497eb2"),
               EmployeeContractId = Guid.Parse("42c05e21-2931-4d71-8735-1f17508621a7"),
               Name = "test",
               Type = AllowanceType.Meal_Allowance,
               Amount = 1200000,
               Eligibility_Criteria = "test",
               Requirements = "test",
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

        builder.Entity<Company>()
        .HasData(
            new Company
            {
                Id = Guid.Parse("6b36c601-4a2b-47a2-b607-3597f3049f75"),
                Name = "CÔNG TY A",
                Address = "Quận 9, Tp HCM",
                AccountEmail = "congtya@gmail.com",
                Phone = "0987654321",
                HREmail = "HRcongtyA@gmail.com",
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Company
            {
                Id = Guid.Parse("441d5aa8-9a63-49f5-9e4c-954c951e369d"),
                Name = "CÔNG TY B",
                Address = "Quận 10, Tp HCM",
                AccountEmail = "congtyb@gmail.com",
                Phone = "0123456789",
                HREmail = "HRcongtyB@gmail.com",
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            });
        builder.Entity<JobDescription>()
       .HasData(
           new JobDescription
           {
               Id = Guid.Parse("441d5aa8-9a63-49f5-9e4c-954c951e369d"),
               CompanyId = Guid.Parse("6b36c601-4a2b-47a2-b607-3597f3049f75"),
               Title = "Job Back-end C#",
               Description = "Thích thì dô làm",
               StartDate = new DateTime(9999, 9, 9, 0, 0, 0),
               EndDate = new DateTime(9999, 9, 9, 0, 0, 0),
               Status = "Đang tuyển dụng",
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },
            new JobDescription
            {
                Id = Guid.Parse("06a99eff-b374-4a5e-a3fc-58e8defecfe6"),
                CompanyId = Guid.Parse("441d5aa8-9a63-49f5-9e4c-954c951e369d"),
                Title = "Job Back-end Java",
                Description = "Doooooooooooooooooooooooo",
                StartDate = new DateTime(9999, 9, 9, 0, 0, 0),
                EndDate = new DateTime(9999, 9, 9, 0, 0, 0),
                Status = "Đang tuyển dụng",
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            });

        builder.Entity<Skill_JD>()
       .HasData(
           new Skill_JD
           {
               Id = Guid.Parse("bb499b5d-69f1-4a9e-958a-f1c3bebd7350"),
               SkillId = Guid.Parse("34647606-a482-47e5-a59b-66cfbc5b66ac"),
               JobDescriptionId = Guid.Parse("06a99eff-b374-4a5e-a3fc-58e8defecfe6"),
               Level = "Tạm",
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },
           new Skill_JD
           {
               Id = Guid.Parse("318d7891-7119-4543-ab13-e4e61333ea08"),
               SkillId = Guid.Parse("34647606-a482-47e5-a59b-66cfbc5b66ac"),
               JobDescriptionId = Guid.Parse("441d5aa8-9a63-49f5-9e4c-954c951e369d"),
               Level = "Đỉnh kao",
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           });

        builder.Entity<TaxInCome>()
       .HasData(
           new TaxInCome

           {
               Id = Guid.Parse("a279788d-0fa2-4d9e-9e8e-5d689e853972"),
               Muc_chiu_thue = 5000000,
               Thue_suat = 0.05,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"),
               Muc_chiu_thue = 10000000,
               Thue_suat = 0.1,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("e582dd24-ec47-4c64-b0a7-6f8647b488a7"),
               Muc_chiu_thue = 18000000,
               Thue_suat = 0.15,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"),
               Muc_chiu_thue = 32000000,
               Thue_suat = 0.2,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"),
               Muc_chiu_thue = 52000000,
               Thue_suat = 0.25,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("78a65c98-2d7a-4c57-98f0-81f5a870a915"),
               Muc_chiu_thue = 80000000,
               Thue_suat = 0.3,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           },

           new TaxInCome

           {
               Id = Guid.Parse("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"),
               Muc_chiu_thue = double.MaxValue,
               Thue_suat = 0.35,
               IsDeleted = false,
               Created = new DateTime(9999, 9, 9, 0, 0, 0),
               CreatedBy = "test",
               LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
               LastModifiedBy = "test"
           });

        builder.Entity<Exchange>()
        .HasData(
            new Exchange
            {
                Id = Guid.Parse("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"),
                Muc_Quy_Doi = 4750000,
                Giam_Tru = 0,
                Thue_Suat = 0.95,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"),
                Muc_Quy_Doi = 9250000,
                Giam_Tru = 250000,
                Thue_Suat = 0.9,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"),
                Muc_Quy_Doi = 16050000,
                Giam_Tru = 750000,
                Thue_Suat = 0.85,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("9218741c-99f6-40a2-87f4-d4baf4c9e15d"),
                Muc_Quy_Doi = 27250000,
                Giam_Tru = 1650000,
                Thue_Suat = 0.8,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("e28a08ad-2b30-4df5-bc95-684d56ad8a56"),
                Muc_Quy_Doi = 42250000,
                Giam_Tru = 3250000,
                Thue_Suat = 0.75,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"),
                Muc_Quy_Doi = 61850000,
                Giam_Tru = 5850000,
                Thue_Suat = 0.7,
                IsDeleted = false,
                Created = new DateTime(9999, 9, 9, 0, 0, 0),
                CreatedBy = "test",
                LastModified = new DateTime(9999, 9, 9, 0, 0, 0),
                LastModifiedBy = "test"
            },
            new Exchange
            {
                Id = Guid.Parse("7f1b1d11-3070-4b4b-96db-801d448a8920"),
                Muc_Quy_Doi = double.MaxValue,
                Giam_Tru = 9850000,
                Thue_Suat = 0.65,
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