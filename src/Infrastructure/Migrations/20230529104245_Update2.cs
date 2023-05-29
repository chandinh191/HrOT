using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Allowances",
                keyColumn: "Id",
                keyValue: new Guid("c0d544cb-a345-490d-8ba3-d1c63e497eb2"),
                column: "Eligibility_Criteria",
                value: "test");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "AccountEmail", "Address", "Created", "CreatedBy", "HREmail", "IsDeleted", "LastModified", "LastModifiedBy", "Name", "Phone" },
                values: new object[,]
                {
                    { new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), "congtyb@gmail.com", "Quận 10, Tp HCM", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "HRcongtyB@gmail.com", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "CÔNG TY B", "0123456789" },
                    { new Guid("6b36c601-4a2b-47a2-b607-3597f3049f75"), "congtya@gmail.com", "Quận 9, Tp HCM", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "HRcongtyA@gmail.com", false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "CÔNG TY A", "0987654321" }
                });

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "Id",
                keyValue: new Guid("42c05e21-2931-4d71-8735-1f17508621a7"),
                column: "Number_Of_Dependents",
                value: 0.0);

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

            migrationBuilder.InsertData(
                table: "JobDescriptions",
                columns: new[] { "Id", "CompanyId", "Created", "CreatedBy", "Description", "EndDate", "IsDeleted", "LastModified", "LastModifiedBy", "StartDate", "Status", "Title" },
                values: new object[,]
                {
                    { new Guid("06a99eff-b374-4a5e-a3fc-58e8defecfe6"), new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Doooooooooooooooooooooooo", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang tuyển dụng", "Job Back-end Java" },
                    { new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new Guid("6b36c601-4a2b-47a2-b607-3597f3049f75"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Thích thì dô làm", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang tuyển dụng", "Job Back-end C#" }
                });

            migrationBuilder.InsertData(
                table: "Skill_JDs",
                columns: new[] { "Id", "Created", "CreatedBy", "IsDeleted", "JobDescriptionId", "LastModified", "LastModifiedBy", "Level", "SkillId" },
                values: new object[,]
                {
                    { new Guid("318d7891-7119-4543-ab13-e4e61333ea08"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Đỉnh kao", new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac") },
                    { new Guid("bb499b5d-69f1-4a9e-958a-f1c3bebd7350"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", false, new Guid("06a99eff-b374-4a5e-a3fc-58e8defecfe6"), new DateTime(9999, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "test", "Tạm", new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Skill_JDs",
                keyColumn: "Id",
                keyValue: new Guid("318d7891-7119-4543-ab13-e4e61333ea08"));

            migrationBuilder.DeleteData(
                table: "Skill_JDs",
                keyColumn: "Id",
                keyValue: new Guid("bb499b5d-69f1-4a9e-958a-f1c3bebd7350"));

            migrationBuilder.DeleteData(
                table: "JobDescriptions",
                keyColumn: "Id",
                keyValue: new Guid("06a99eff-b374-4a5e-a3fc-58e8defecfe6"));

            migrationBuilder.DeleteData(
                table: "JobDescriptions",
                keyColumn: "Id",
                keyValue: new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("441d5aa8-9a63-49f5-9e4c-954c951e369d"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6b36c601-4a2b-47a2-b607-3597f3049f75"));

            migrationBuilder.UpdateData(
                table: "Allowances",
                keyColumn: "Id",
                keyValue: new Guid("c0d544cb-a345-490d-8ba3-d1c63e497eb2"),
                column: "Eligibility_Criteria",
                value: null);

            migrationBuilder.UpdateData(
                table: "EmployeeContracts",
                keyColumn: "Id",
                keyValue: new Guid("42c05e21-2931-4d71-8735-1f17508621a7"),
                column: "Number_Of_Dependents",
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
