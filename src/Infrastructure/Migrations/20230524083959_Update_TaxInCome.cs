using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTaxInCome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Muc_chiu_thue",
                table: "DetailTaxIncomes");

            migrationBuilder.DropColumn(
                name: "Thue_suat",
                table: "DetailTaxIncomes");

            migrationBuilder.AddColumn<double>(
                name: "Giam_Tru",
                table: "Exchanges",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Thue_Suat",
                table: "Exchanges",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Payment",
                table: "DetailTaxIncomes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TaxIncomes",
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
                    table.PrimaryKey("PK_TaxIncomes", x => x.Id);
                });

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

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                column: "Skill_Description",
                value: "TEST");

            migrationBuilder.InsertData(
                table: "TaxIncomes",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxIncomes");

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("08d5ef29-44a5-47d0-9a85-28d9d0dc30f3"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("3a1d42d9-9ee7-4a4c-b283-6e8f9126e44a"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("5ef5f6be-ef53-4af2-8b18-78fc6b8a295f"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("7f1b1d11-3070-4b4b-96db-801d448a8920"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("9218741c-99f6-40a2-87f4-d4baf4c9e15d"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("d9f3a521-36e4-4a5c-9a89-0c733e92e85b"));

            migrationBuilder.DeleteData(
                table: "Exchanges",
                keyColumn: "Id",
                keyValue: new Guid("e28a08ad-2b30-4df5-bc95-684d56ad8a56"));

            migrationBuilder.DropColumn(
                name: "Giam_Tru",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Thue_Suat",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "DetailTaxIncomes");

            migrationBuilder.AddColumn<string>(
                name: "Muc_chiu_thue",
                table: "DetailTaxIncomes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Thue_suat",
                table: "DetailTaxIncomes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Skills",
                keyColumn: "Id",
                keyValue: new Guid("34647606-a482-47e5-a59b-66cfbc5b66ac"),
                column: "Skill_Description",
                value: null);
        }
    }
}
