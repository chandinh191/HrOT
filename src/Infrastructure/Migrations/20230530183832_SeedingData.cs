using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnualWorkingDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Coefficients = table.Column<double>(type: "float", nullable: false),
                    TypeDate = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualWorkingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HREmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceCodes",
                columns: table => new
                {
                    UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MucQuyDoi = table.Column<double>(name: "Muc_Quy_Doi", type: "float", nullable: true),
                    GiamTru = table.Column<double>(name: "Giam_Tru", type: "float", nullable: true),
                    ThueSuat = table.Column<double>(name: "Thue_Suat", type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersistedGrants",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersistedGrants", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SkillDescription = table.Column<string>(name: "Skill_Description", type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaxInComes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mucchiuthue = table.Column<double>(name: "Muc_chiu_thue", type: "float", nullable: true),
                    Thuesuat = table.Column<double>(name: "Thue_suat", type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxInComes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ColourCode = table.Column<string>(name: "Colour_Code", type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Day = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobDescriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDescriptions_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Done = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_JDs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill_JDs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_JDs_JobDescriptions_JobDescriptionId",
                        column: x => x.JobDescriptionId,
                        principalTable: "JobDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_JDs_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allowances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    EligibilityCriteria = table.Column<string>(name: "Eligibility_Criteria", type: "nvarchar(max)", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allowances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InterviewProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyContracts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PaymentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalDeduction = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalBonus = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    AcceptanceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentHistories_CompanyContracts_CompanyContractId",
                        column: x => x.CompanyContractId,
                        principalTable: "CompanyContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CitizenIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateCIN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlaceForCIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CVPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Levels_Positions_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    NumberOfDependents = table.Column<double>(name: "Number_Of_Dependents", type: "float", nullable: true),
                    CustomSalary = table.Column<double>(type: "float", nullable: true),
                    InsuranceType = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    SalaryType = table.Column<int>(type: "int", nullable: true),
                    ContractType = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeContracts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NameProject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TeamSize = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechStack = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfDependents = table.Column<int>(type: "int", nullable: true),
                    HomeTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Families_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobDescriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DayTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeedBack = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewProcesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewProcesses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewProcesses_JobDescriptions_JobDescriptionId",
                        column: x => x.JobDescriptionId,
                        principalTable: "JobDescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveHours = table.Column<double>(type: "float", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OvertimeLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OvertimeLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OvertimeLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skill_Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SkillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_Employees_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_Employees_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeAttendanceLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ducation = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeAttendanceLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TimeAttendanceLogs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaySlips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeContractId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StandardWorkHours = table.Column<int>(name: "Standard_Work_Hours", type: "int", nullable: true),
                    ActualWorkHours = table.Column<int>(name: "Actual_Work_Hours", type: "int", nullable: true),
                    OtHours = table.Column<int>(name: "Ot_Hours", type: "int", nullable: true),
                    LeaveHours = table.Column<int>(name: "Leave_Hours", type: "int", nullable: true),
                    Salary = table.Column<double>(type: "float", nullable: true),
                    BHXHEmp = table.Column<double>(name: "BHXH_Emp", type: "float", nullable: true),
                    BHYTEmp = table.Column<double>(name: "BHYT_Emp", type: "float", nullable: true),
                    BHTNEmp = table.Column<double>(name: "BHTN_Emp", type: "float", nullable: true),
                    BHXHComp = table.Column<double>(name: "BHXH_Comp", type: "float", nullable: true),
                    BHYTComp = table.Column<double>(name: "BHYT_Comp", type: "float", nullable: true),
                    BHTNComp = table.Column<double>(name: "BHTN_Comp", type: "float", nullable: true),
                    TaxInCome = table.Column<double>(name: "Tax_In_Come", type: "float", nullable: true),
                    Bonus = table.Column<double>(type: "float", nullable: true),
                    Deduction = table.Column<double>(type: "float", nullable: true),
                    TotalAllowance = table.Column<double>(name: "Total_Allowance", type: "float", nullable: true),
                    FinalSalary = table.Column<double>(name: "Final_Salary", type: "float", nullable: true),
                    CompanyPaid = table.Column<double>(name: "Company_Paid", type: "float", nullable: true),
                    Paiddate = table.Column<DateTime>(name: "Paid_date", type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAcountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAcountNumber = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaySlips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaySlips_EmployeeContracts_EmployeeContractId",
                        column: x => x.EmployeeContractId,
                        principalTable: "EmployeeContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetailTaxIncomes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaySlipId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Payment = table.Column<double>(type: "float", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailTaxIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailTaxIncomes_PaySlips_PaySlipId",
                        column: x => x.PaySlipId,
                        principalTable: "PaySlips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDay", "ConcurrencyStamp", "Email", "EmailConfirmed", "Fullname", "Image", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fe30e976-2640-4d35-8334-88e7c3b1eac1", 0, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "40495f9c-e853-41e8-8c5b-6b3c93d3791b", "test@gmail.com", true, "Lewis", "TESTIMAGE", true, new DateTimeOffset(new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 7, 0, 0, 0)), "test@gmail.com", "test", "AQAAAAIAAYagAAAAEFNwXlIXp0mbDE5k1gIQdlbAczn8BwINQnF5S0qULxDK/6luT/bumpD+HFOXM0k59A==", "123456789", true, "VEPOTJNXQCZMK3J7R27HMLXD64T72GU6", false, "admin" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AccountEmail", "Address", "Created", "CreatedBy", "HREmail", "IsDeleted", "LastModified", "LastModifiedBy", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), "congtyb@gmail.com", "Quận 10, Tp HCM", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "HRcongtyB@gmail.com", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "CÔNG TY B", "0123456789" },
                    { new Guid("6b36c601-4a2b-47a2-b607-3597f3049f75"), "congtya@gmail.com", "Quận 9, Tp HCM", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "HRcongtyA@gmail.com", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "CÔNG TY A", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894142"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Đảm nhận công việc liên quan phần mềm", null, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Phòng IT" });

            migrationBuilder.InsertData(
                table: "Exchanges",
                columns: new[] { "Id", "Created", "CreatedBy", "Giam_Tru", "IsDeleted", "LastModified", "LastModifiedBy", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[,]
                {
                    { new Guid("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 750000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 16050000.0, 0.84999999999999998 },
                    { new Guid("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 250000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 9250000.0, 0.90000000000000002 },
                    { new Guid("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 5850000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 61850000.0, 0.69999999999999996 },
                    { new Guid("7f1b1d11-3070-4b4b-96db-801d448a8920"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 9850000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1.7976931348623157E+308, 0.65000000000000002 },
                    { new Guid("9218741c-99f6-40a2-87f4-d4baf4c9e15d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1650000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 27250000.0, 0.80000000000000004 },
                    { new Guid("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 0.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 4750000.0, 0.94999999999999996 },
                    { new Guid("e28a08ad-2b30-4df5-bc95-684d56ad8a56"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 3250000.0, false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 42250000.0, 0.75 }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "LastModified", "LastModifiedBy", "SkillName", "Skill_Description" },
                values: new object[] { new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "TESTSKILL", "TEST" });

            migrationBuilder.InsertData(
                table: "TaxInComes",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "LastModified", "LastModifiedBy", "Muc_chiu_thue", "Thue_suat" },
                values: new object[,]
                {
                    { new Guid("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 10000000.0, 0.10000000000000001 },
                    { new Guid("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1.7976931348623157E+308, 0.34999999999999998 },
                    { new Guid("78a65c98-2d7a-4c57-98f0-81f5a870a915"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 80000000.0, 0.29999999999999999 },
                    { new Guid("a279788d-0fa2-4d9e-9e8e-5d689e853972"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 5000000.0, 0.050000000000000003 },
                    { new Guid("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 32000000.0, 0.20000000000000001 },
                    { new Guid("e582dd24-ec47-4c64-b0a7-6f8647b488a7"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 18000000.0, 0.14999999999999999 },
                    { new Guid("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 52000000.0, 0.25 }
                });

            migrationBuilder.InsertData(
                table: "JobDescriptions",
                columns: new[] { "Id", "CompanyId", "Created", "CreatedBy", "Description", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "StartDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("06a99eff-b374-4a5e-a3fc-58e8defecfe6"), new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Doooooooooooooooooooooooo", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang tuyển dụng", "Job Back-end Java" },
                    { new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new Guid("6b36c601-4a2b-47a2-b607-3597f3049f75"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Thích thì dô làm", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang tuyển dụng", "Job Back-end C#" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Created", "CreatedBy", "DepartmentId", "IsDeleted", "LastModified", "LastModifiedBy", "Name" },
                values: new object[] { new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894143"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894142"), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Nhân viên" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Address", "ApplicationUserId", "BankAccountName", "BankAccountNumber", "BankName", "CVPath", "CitizenIdentificationNumber", "Created", "CreatedBy", "CreatedDateCIN", "District", "IsDeleted", "LastModified", "LastModifiedBy", "PlaceForCIN", "PositionId", "Province" },
                values: new object[] { new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), "123, Lê Văn Việt, Tăng Nhơn Phú", "fe30e976-2640-4d35-8334-88e7c3b1eac1", "LUONG THE DAN", "123456789", "TECHCOMBANK", null, "0931248141241231", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quận nine", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "TP HCM", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894143"), "TP Hồ Chí Minh" });

            migrationBuilder.InsertData(
                table: "Skill_JDs",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "JobDescriptionId", "LastModified", "LastModifiedBy", "Level", "SkillId" },
                values: new object[,]
                {
                    { new Guid("318d7891-7119-4543-ab13-e4e61333ea08"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Đỉnh kao", new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac") },
                    { new Guid("bb499b5d-69f1-4a9e-958a-f1c3bebd7350"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new Guid("06a99eff-b374-4a5e-a3fc-58e8defecfe6"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Tạm", new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac") }
                });

            migrationBuilder.InsertData(
                table: "Degrees",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Name", "Status" },
                values: new object[] { new Guid("d10de52b-58a1-43e8-9ab6-5651693341f8"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Test", 1 });

            migrationBuilder.InsertData(
                table: "EmployeeContracts",
                columns: new[] { "Id", "ContractType", "Created", "CreatedBy", "CustomSalary", "EmployeeId", "EndDate", "File", "InsuranceType", "IsDeleted", "Job", "LastModified", "LastModifiedBy", "Number_Of_Dependents", "Salary", "SalaryType", "StartDate", "Status" },
                values: new object[] { new Guid("42c05e21-2931-4d71-8735-1f17508621a7"), 1, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 0.0, new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 0, false, "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 0.0, 20000000.0, 0, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "Experiences",
                columns: new[] { "Id", "Created", "CreatedBy", "Description", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "NameProject", "StartDate", "Status", "TeamSize", "TechStack" },
                values: new object[] { new Guid("850df2d9-f8dc-444a-b1dc-ca773c0a2d0d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Normal", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "TestProject", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, "MSSQL, .NET 7, MVC" });

            migrationBuilder.InsertData(
                table: "Families",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "FatherName", "HomeTown", "IsDeleted", "LastModified", "LastModifiedBy", "MotherName", "NumberOfDependents" },
                values: new object[] { new Guid("668d6b8b-7997-40fc-9454-036158af413b"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), "Test", "Test", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Test", 3 });

            migrationBuilder.InsertData(
                table: "LeaveLogs",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "LeaveHours", "Reason", "StartDate", "Status" },
                values: new object[] { new Guid("93addd6c-7337-4b42-ab46-7f8b51405ea9"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", 1.0, "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0 });

            migrationBuilder.InsertData(
                table: "OvertimeLogs",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "StartDate", "Status", "TotalHours" },
                values: new object[] { new Guid("36cb5368-1b04-4b23-bdc0-2949ae3568d7"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 9.0 });

            migrationBuilder.InsertData(
                table: "Skill_Employees",
                columns: new[] { "Id", "Created", "CreatedBy", "EmployeeId", "IsDeleted", "LastModified", "LastModifiedBy", "Level", "SkillId" },
                values: new object[] { new Guid("eaee3dcb-bfbb-497c-acff-6d013ef0ffd8"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new Guid("ac69dc8e-f88d-46c2-a861-c9d5ac894141"), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Immediate", new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac") });

            migrationBuilder.InsertData(
                table: "Allowances",
                columns: new[] { "Id", "Amount", "Created", "CreatedBy", "Eligibility_Criteria", "EmployeeContractId", "IsDeleted", "LastModified", "LastModifiedBy", "Name", "Requirements", "Type" },
                values: new object[] { new Guid("c0d544cb-a345-490d-8ba3-d1c63e497eb2"), 1200000.0, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "test", new Guid("42c05e21-2931-4d71-8735-1f17508621a7"), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "test", "test", 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Allowances_EmployeeContractId",
                table: "Allowances",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContracts_CompanyId",
                table: "CompanyContracts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContracts_InterviewProcessId",
                table: "CompanyContracts",
                column: "InterviewProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Degrees_EmployeeId",
                table: "Degrees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetailTaxIncomes_PaySlipId",
                table: "DetailTaxIncomes",
                column: "PaySlipId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_DeviceCode",
                table: "DeviceCodes",
                column: "DeviceCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceCodes_Expiration",
                table: "DeviceCodes",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContracts_EmployeeId",
                table: "EmployeeContracts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ApplicationUserId",
                table: "Employees",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_EmployeeId",
                table: "Experiences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_EmployeeId",
                table: "Families",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CompanyId",
                table: "Holidays",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewProcesses_EmployeeId",
                table: "InterviewProcesses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewProcesses_JobDescriptionId",
                table: "InterviewProcesses",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDescriptions_CompanyId",
                table: "JobDescriptions",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveLogs_EmployeeId",
                table: "LeaveLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Levels_RoleId",
                table: "Levels",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_OvertimeLogs_EmployeeId",
                table: "OvertimeLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentHistories_CompanyContractId",
                table: "PaymentHistories",
                column: "CompanyContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PaySlips_EmployeeContractId",
                table: "PaySlips",
                column: "EmployeeContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_Expiration",
                table: "PersistedGrants",
                column: "Expiration");

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_ClientId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "ClientId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_SubjectId_SessionId_Type",
                table: "PersistedGrants",
                columns: new[] { "SubjectId", "SessionId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId",
                table: "Positions",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Employees_EmployeeId",
                table: "Skill_Employees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_Employees_SkillId",
                table: "Skill_Employees",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_JDs_JobDescriptionId",
                table: "Skill_JDs",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_JDs_SkillId",
                table: "Skill_JDs",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeAttendanceLogs_EmployeeId",
                table: "TimeAttendanceLogs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allowances_EmployeeContracts_EmployeeContractId",
                table: "Allowances",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyContracts_InterviewProcesses_InterviewProcessId",
                table: "CompanyContracts",
                column: "InterviewProcessId",
                principalTable: "InterviewProcesses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Degrees_Employees_EmployeeId",
                table: "Degrees",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_EmployeeId",
                table: "Departments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_ApplicationUserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_EmployeeId",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Allowances");

            migrationBuilder.DropTable(
                name: "AnnualWorkingDays");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "DetailTaxIncomes");

            migrationBuilder.DropTable(
                name: "DeviceCodes");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "LeaveLogs");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "OvertimeLogs");

            migrationBuilder.DropTable(
                name: "PaymentHistories");

            migrationBuilder.DropTable(
                name: "PersistedGrants");

            migrationBuilder.DropTable(
                name: "Skill_Employees");

            migrationBuilder.DropTable(
                name: "Skill_JDs");

            migrationBuilder.DropTable(
                name: "TaxInComes");

            migrationBuilder.DropTable(
                name: "TimeAttendanceLogs");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PaySlips");

            migrationBuilder.DropTable(
                name: "CompanyContracts");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TodoLists");

            migrationBuilder.DropTable(
                name: "EmployeeContracts");

            migrationBuilder.DropTable(
                name: "InterviewProcesses");

            migrationBuilder.DropTable(
                name: "JobDescriptions");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
