using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Allowances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Allowances",
                keyColumn: "Id",
                keyValue: new Guid("c0d544cb-a345-490d-8ba3-d1c63e497eb2"),
                columns: new[] { "Eligibility_Criteria", "EmployeeId" },
                values: new object[] { "test", new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 750000.0, 16050000.0, 0.84999999999999998 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 250000.0, 9250000.0, 0.90000000000000002 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 5850000.0, 61850000.0, 0.69999999999999996 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("7f1b1d11-3070-4b4b-96db-801d448a8920"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 9850000.0, 1.7976931348623157E+308, 0.65000000000000002 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("9218741c-99f6-40a2-87f4-d4baf4c9e15d"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 1650000.0, 27250000.0, 0.80000000000000004 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 0.0, 4750000.0, 0.94999999999999996 });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("e28a08ad-2b30-4df5-bc95-684d56ad8a56"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { 3250000.0, 42250000.0, 0.75 });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                column: "Skill_Description",
                value: "TEST");

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 10000000.0, 0.10000000000000001 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 1.7976931348623157E+308, 0.34999999999999998 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("78a65c98-2d7a-4c57-98f0-81f5a870a915"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 80000000.0, 0.29999999999999999 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("a279788d-0fa2-4d9e-9e8e-5d689e853972"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 5000000.0, 0.050000000000000003 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 32000000.0, 0.20000000000000001 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("e582dd24-ec47-4c64-b0a7-6f8647b488a7"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 18000000.0, 0.14999999999999999 });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { 52000000.0, 0.25 });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_EmployeeId",
                table: "BankAccounts",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Allowances");

            migrationBuilder.UpdateData(
                table: "Allowances",
                keyColumn: "Id",
                keyValue: new Guid("c0d544cb-a345-490d-8ba3-d1c63e497eb2"),
                column: "Eligibility_Criteria",
                value: null);

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("7f1b1d11-3070-4b4b-96db-801d448a8920"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("9218741c-99f6-40a2-87f4-d4baf4c9e15d"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("e28a08ad-2b30-4df5-bc95-684d56ad8a56"),
                columns: new[] { "Giam_Tru", "Muc_Quy_Doi", "Thue_Suat" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                column: "Skill_Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("2d6a8e64-6130-456b-9c9d-95a1bc0a11fd"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("4ae0e892-5369-4ef1-9e37-5d4e0a9a3e2e"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("78a65c98-2d7a-4c57-98f0-81f5a870a915"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("a279788d-0fa2-4d9e-9e8e-5d689e853972"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("c0b17a9e-ee6f-4fe0-8e6f-33d5c63640c8"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("e582dd24-ec47-4c64-b0a7-6f8647b488a7"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "TaxInComes",
                keyColumn: "Id",
                keyValue: new Guid("f0f3e78c-67c9-4e5e-a9fc-c8d2e7c1e5f5"),
                columns: new[] { "Muc_chiu_thue", "Thue_suat" },
                values: new object[] { null, null });
        }
    }
}
